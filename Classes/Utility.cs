using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFavoriteEncryption.Classes
{
  public class Utility
  {
    public static string ContentTypes(string ext) =>
             ext switch
             {
               ".txt" => "text/plain",
               ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
               _ => "multipart/mixed"
             };
  }
  }
