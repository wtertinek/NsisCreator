using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator
{
  // TODO: Possibly not the best name, but has to be distinguishable from Directory.
  public class AdditionalDirectory
  {
    public AdditionalDirectory()
    {
      ShortCuts = new List<ShortCut>();
    }

    public string Path { get; set; }

    public List<ShortCut> ShortCuts { get; set; }

    public void AppendInstall(StringBuilder builder)
    {
      builder.AppendLine(2, "CreateDirectory \"" + Path + "\"");

      if (ShortCuts.Any())
      {
        builder.AppendLine();
      }

      foreach (var shortCut in ShortCuts)
      {
        shortCut.AppendInstall(builder);
      }
    }

    public void AppendUninstall(StringBuilder builder)
    {
      foreach (var shortCut in ShortCuts)
      {
        shortCut.AppendUninstall(builder);
      }
    }
  }
}
