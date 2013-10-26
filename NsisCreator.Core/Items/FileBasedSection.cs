using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator
{
  [DebuggerDisplay("{Name}")]
  public class FileBasedSection : SectionBase
  {
    public FileBasedSection()
    {
      Directories = new Directory[0];
      FileGroups = new FileGroup[0];
      ShortCuts = new ShortCut[0];
    }

    public Directory[] Directories { get; set; }

    public FileGroup[] FileGroups { get; set; }

    public ShortCut[] ShortCuts { get; set; }

    protected override void AppendInstallBodyBegin(StringBuilder builder)
    {
      base.AppendInstallBodyBegin(builder);

      if (Directories.Any() || FileGroups.Any())
      {
        builder.AppendLine(2, "SetOutPath \"{0}\"", OutDir);
      }
    }

    protected override void AppendInstallBodyMain(StringBuilder builder)
    {
      var overwrite = OverwriteMode.On;

      foreach (var directory in Directories)
      {
        builder.AppendLine();
        overwrite = directory.AppendInstall(builder, overwrite);
      }

      if (FileGroups.Any())
      {
        builder.AppendLine();
      }

      foreach (var fileGroup in FileGroups)
      {
        overwrite = fileGroup.AppendInstall(builder, overwrite);
      }

      if (ShortCuts.Any())
      {
        builder.AppendLine();
      }

      var shortCutPaths = ShortCuts.Where(s => s.ShortCutPath.Count(c => c.Equals('\\')) > 1)
                                   .Select(s => System.IO.Path.GetDirectoryName(s.ShortCutPath))
                                   .Distinct();

      foreach (var shortCutPath in shortCutPaths)
      {
        builder.AppendLine(2, "CreateDirectory \"{0}\"", shortCutPath);
      }

      if (shortCutPaths.Any())
      {
        builder.AppendLine();
      }

      foreach (var shortCut in ShortCuts)
      {
        shortCut.AppendInstall(builder);
      }

      base.AppendInstallBodyMain(builder);
    }

    protected override void AppendUninstallBodyMain(StringBuilder builder)
    {
      foreach (var shortCut in ShortCuts)
      {
        shortCut.AppendUninstall(builder);
      }

      foreach (var file in FileGroups)
      {
        file.AppendUninstall(builder, OutDir);
      }

      foreach (var directory in Directories)
      {
        directory.AppendUninstall(builder, OutDir);
      }

      base.AppendUninstallBodyMain(builder);
    }

    public IEnumerable<string> GetPathsToRemove()
    {
      var pathsToRemove = new List<string>();
      pathsToRemove.AddRange(GetPaths(ShortCuts.Select(s => System.IO.Path.GetDirectoryName(s.ShortCutPath.Trim('\"')))));
      pathsToRemove.AddRange(GetPaths(from file in FileGroups.SelectMany(g => g.Files)
                                      where !string.IsNullOrEmpty(file.TargetName) &&
                                            file.TargetName.Contains("\\")
                                      select System.IO.Path.GetDirectoryName(System.IO.Path.Combine(OutDir, file.TargetName))));
      pathsToRemove.AddRange(GetPaths(new [] { OutDir }));
      return pathsToRemove.Distinct()
                          .Where(p => p.Count(c => c == '\\') > 0)
                          .OrderByDescending(p => p.Length);
    }

    private IEnumerable<string> GetPaths(IEnumerable<string> directories)
    {
      return directories.Distinct()
                        .SelectMany<string, string>(p =>
                                                    {
                                                      var paths = new List<string>();
                                                      var split = p.Split('\\');

                                                      for (int i = 0; i < split.Length; i++)
                                                      {
                                                        var tmp = new StringBuilder();
                                                        tmp.Append(split[0]);

                                                        for (int j = 1; j < split.Length - i; j++)
                                                        {
                                                          tmp.Append("\\" + split[j]);
                                                        }

                                                        paths.Add(tmp.ToString());
                                                      }

                                                      return paths;
                                                    });

    }
  }
}
