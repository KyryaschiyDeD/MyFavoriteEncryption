using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;

namespace MyFavoriteEncryption.Classes
{
  public class JobFiles
  {
    private static string directory = AppDomain.CurrentDomain.BaseDirectory + "/Files/";
    public List<FileInfo> ListFiles = new List<FileInfo>();
    public static string AddFile(IFormFile uploadedFile)
    {
      string path = "";
      if (uploadedFile != null)
      {
        if (!Directory.Exists(directory))
          Directory.CreateDirectory(directory);
        path = directory + uploadedFile.FileName;

        using (var fileStream = File.Create(path))
        {
          uploadedFile.CopyTo(fileStream);
        }


      }
      if (File.Exists(path))
        return path;
      else
        return null;
    }
    public static string ReadFile(string path)
    {
      if (File.Exists(path))
      {
        FileInfo fileInf = new FileInfo(path);
        switch (fileInf.Extension)
        {
          case ".txt":
            return ReadTxt(path);
          case ".docx":
            return ReadDocx(path);
        }
      }
      return null;
    }
    private static string ReadTxt(string path)
    {
      string data = "";
      var srcEncoding = Encoding.Default;
      Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
      try
      {
        using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding(1251), true))
        {
          data = sr.ReadToEnd();
        }
      }
      catch (Exception ex)
      {
        return ex.Message;
      }
      return data;
    }
    private static string ReadDocx(string path)
    {
      string data = "";
      var srcEncoding = Encoding.Default;
      Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
      try
      {
        DocxToText dtt = new DocxToText(path);
        data = dtt.ExtractText();
      }
      catch (Exception ex)
      {
        return ex.Message;
      }
      return data;
    }
    private static string CreateTxtFile(string data)
    {
      string path = directory + "result.txt";
      StreamWriter sw = new StreamWriter(path);
      sw.WriteLine(data);
      sw.Close();
      return path;
    }
    /*private static string CreateDocxFile(string data)  Не получилось, не успел
    {
      string fileName = directory + "result.docx";

      var doc = DocX.Create(fileName);

      doc.InsertParagraph(data);

      doc.Save();
      return fileName;
    } */

    public static string GetFile(string resultText, string typeFileSelect)
    {
      switch (typeFileSelect)
      {
        case "txt":
          return CreateTxtFile(resultText);
          // Не получилось, не успел
        /*case "docx":   
          return CreateDocxFile(resultText); */ 
      }
      return null;
    }

  }
}
