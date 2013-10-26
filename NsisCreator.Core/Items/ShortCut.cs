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
      ShortCutPath = "";
      TargetPath = "";
      Argument = "";
    }

    public string ShortCutPath { get; set; }

    public string TargetPath { get; set; }

    public string Argument { get; set; }

    public void AppendInstall(StringBuilder builder)
    {
      builder.AppendFormat(2, "CreateShortCut \"{0}\" \"{1}\"", ShortCutPath, TargetPath);

      if (!string.IsNullOrEmpty(Argument))
      {
        builder.AppendFormat(" \"{0}\"", Argument);
      }

      builder.Append("\n");
    }

    public void AppendUninstall(StringBuilder builder)
    {
      builder.AppendLine(2, "Delete \"{0}\"", ShortCutPath);
    }

    public static ShortCut CreateUninstall(string shortCutPath, params object[] args)
    {
      return Create(string.Format(shortCutPath, args), @"$INSTDIR\uninst.exe", "");
    }

    public static ShortCut Create(string shortCutPath, string targetPath, string argument)
    {
      return new ShortCut()
      {
        ShortCutPath = shortCutPath,
        TargetPath = targetPath,
        Argument = argument
      };
    }
  }
}
