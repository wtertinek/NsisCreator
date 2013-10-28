using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator
{
  public class InputDirectory
  {
    public InputDirectory()
    {
      DirectoryName = "";
      IncludeFilters = new List<Filter>();
      ExcludeFilters = new List<Filter>();
      Overwrite = OverwriteMode.On;
    }

    public string DirectoryName { get; set; }

    public List<Filter> IncludeFilters { get; set; }

    public List<Filter> ExcludeFilters { get; set; }

    public OverwriteMode Overwrite { get; set; }

    private IEnumerable<string> GetFiles()
    {
      var directory = DirectoryName;
      
      if (!System.IO.Path.IsPathRooted(directory))
      {
        directory = System.IO.Path.Combine(Environment.CurrentDirectory, DirectoryName);
      }

      var files = System.IO.Directory.GetFiles(directory, "*.*", System.IO.SearchOption.TopDirectoryOnly);

      files = (from fullFileName in files
               let fileName = System.IO.Path.GetFileName(fullFileName)
               where IncludeFilters.Any(f => f.Match(fileName))
               select fileName).ToArray();

      files = (from fullFileName in files
               let fileName = System.IO.Path.GetFileName(fullFileName)
               where !ExcludeFilters.Any(f => f.Match(fileName))
               select fileName).ToArray();

      return files;
    }

    public OverwriteMode AppendInstall(StringBuilder builder, OverwriteMode currentMode)
    {
      if (currentMode != Overwrite)
      {
        currentMode = Overwrite;
        builder.AppendLine(2, "SetOverwrite {0}", Overwrite);
      }

      foreach (var file in GetFiles())
      {
        builder.AppendLine(2, "File \"{0}\"", System.IO.Path.Combine(DirectoryName, file));
      }

      return currentMode;
    }

    public void AppendUninstall(StringBuilder builder, string outPath)
    {
      foreach (var file in GetFiles().Reverse())
      {
        builder.AppendLine(2, "Delete \"{0}\"", System.IO.Path.Combine(outPath, file));
      }
    }
  }
}
