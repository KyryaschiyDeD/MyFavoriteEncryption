using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFavoriteEncryption.Classes
{
  public class DataInfo
  {
    public string Name { get; set; }
    public int Age { get; set; }
    public string ResultText { get; set; }
    public string ResultRef { get; set; }
    public string UploadedText { get; set; }
    public string ExportText { get; set; }
    public string ErrorText { get; set; }
    public bool AttemptToReadFile { get; set; } = false;
  }
}
