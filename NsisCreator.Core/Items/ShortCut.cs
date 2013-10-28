using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator
{
  public class ShortCut
  {
    public ShortCut()
    {
      Path = "";
      TargetPath = "";
      Argument = "";
    }

    public string Path { get; set; }

    public string TargetPath { get; set; }

    public string Argument { get; set; }

    public void AppendInstall(StringBuilder builder)
    {
      builder.AppendFormat(2, "CreateShortCut \"{0}\" \"{1}\"", Path, TargetPath);

      if (!string.IsNullOrEmpty(Argument))
      {
        builder.AppendFormat(" \"{0}\"", Argument);
      }

      builder.Append("\n");
    }

    public void AppendUninstall(StringBuilder builder)
    {
      builder.AppendLine(2, "Delete \"{0}\"", Path);
    }
  }
}
