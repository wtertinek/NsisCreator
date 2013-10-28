using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator
{
  public class InputFileGroup
  {
    public InputFileGroup()
    {
      Overwrite = OverwriteMode.On;
      Files = new List<InputFile>();
    }

    public OverwriteMode Overwrite { get; set; }

    public List<InputFile> Files { get; set; }

    public OverwriteMode AppendInstall(StringBuilder builder, OverwriteMode currentMode)
    {
      if (currentMode != Overwrite)
      {
        currentMode = Overwrite;
        builder.AppendLine(2, "SetOverwrite {0}", Overwrite);
      }

      foreach (var file in Files)
      {
        file.AppendInstall(builder);
      }

      return currentMode;
    }

    public void AppendUninstall(StringBuilder builder, string outPath)
    {
      foreach (var file in Files)
      {
        file.AppendUninstall(builder, outPath);
      }
    }
  }
}
