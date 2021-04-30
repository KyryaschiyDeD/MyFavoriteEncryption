using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MyFavoriteEncryption.Classes;

namespace MyFavoriteEncryption.Pages
{
  public class TheVigenerCipherModel : PageModel
  {
    public static DataInfo dataInfo = new DataInfo();

    private async Task<IActionResult> StreamSend(FileInfo fi)
    {
      return await Task.Run(() =>
      {
        var stream = fi.OpenRead();
        var contentType = Utility.ContentTypes(fi.Extension);
        return File(stream, contentType, fi.Name);
      });
    }

    public async Task<IActionResult> OnPost(string typeFileSelect)
    {
      FileInfo fi = new FileInfo(JobFiles.GetFile(dataInfo.ResultText, typeFileSelect));

      fi.Refresh();

      if (fi.Exists == true)
      {
        return await StreamSend(fi);
      }
      else
      {
        dataInfo.ErrorText = "Ошибка, файл не найден, либо данные пусты";
      }

      return new EmptyResult();
    }
    public void OnPostDecryptBtn(string textForJob, string keyword)
    {
      dataInfo.UploadedText = textForJob;
      if (String.IsNullOrEmpty(keyword) || !VigenerCipher.CheckingKey(keyword))
      {
        dataInfo.ErrorText = "Ключ пуст или содержет недопустимые символы!!!";
        dataInfo.ResultText = "";
      }
      else
      {
        dataInfo.ErrorText = "";
        dataInfo.ResultText = VigenerCipher.Decode(textForJob, keyword);
      }
    }
    public void OnPostCryptBtn(string textForJob, string keyword)
    {
      dataInfo.UploadedText = textForJob;
      if (String.IsNullOrEmpty(keyword) || !VigenerCipher.CheckingKey(keyword))
      {
        dataInfo.ErrorText = "Ключ пуст или содержет недопустимые символы!!!";
        dataInfo.ResultText = "";
      }
      else
      {
        dataInfo.ErrorText = "";
        dataInfo.ResultText = VigenerCipher.Encode(textForJob, keyword);
      }

    }
    public void OnPostUploadFileBtn(IFormFile uploadedFile)
    {
      dataInfo.AttemptToReadFile = true;
      if (!(uploadedFile.Length > 41943040))
      {
        string path = JobFiles.AddFile(uploadedFile);
        if (path != null)
          dataInfo.UploadedText = JobFiles.ReadFile(path);
      }
    }

  }
}
