using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator
{
  public class File
  {
    public File()
    {
      SourceName = "";
      TargetName = "";
    }

    public string SourceName { get; set; }

    public string TargetName { get; set; }

    public bool KeepFileOnUninstall { get; set; }

    public void AppendInstall(StringBuilder builder)
    {
      if (!string.IsNullOrEmpty(TargetName))
      {
        builder.AppendLine(2, "File \"/oname={0}\" \"{1}\"", TargetName, SourceName);
      }
      else
      {
        builder.AppendLine(2, "File \"{0}\"", SourceName);
      }
    }

    public void AppendUninstall(StringBuilder builder, string outPath)
    {
      if (!KeepFileOnUninstall)
      {
        Action<string> append = name => builder.AppendLine(2, "Delete \"{0}\"", System.IO.Path.Combine(outPath, System.IO.Path.GetFileName(name)));

        if (!string.IsNullOrEmpty(TargetName))
        {
          append(TargetName);
        }
        else
        {
          append(SourceName);
        }
      }
    }
  }
}
