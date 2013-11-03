using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator
{
  [DebuggerDisplay("{Name}")]
  public class Section
  {
    public Section()
    {
      Name = "";
      OutDir = "$INSTDIR";
      Context = ShellVarContext.CurrentUser;
      EnvironmentVariables = new List<EnvironmentVariable>();
      InputDirectories = new List<InputDirectory>();
      InputFileGroups = new List<InputFileGroup>();
      InputFiles = new List<InputFile>();
      Directories = new List<AdditionalDirectory>();
    }

    public string Name { get; set; }

    public string OutDir { get; set; }

    public ShellVarContext Context { get; set; }

    public List<EnvironmentVariable> EnvironmentVariables { get; set; }

    public List<InputDirectory> InputDirectories { get; set; }

    public List<InputFileGroup> InputFileGroups { get; set; }

    public List<InputFile> InputFiles { get; set; }

    public List<AdditionalDirectory> Directories { get; set; }

    public virtual void AppendInstall(StringBuilder builder, int index)
    {
      builder.AppendLine("Section \"{0}\" SEC{1}", Name, index.ToString("D2"));

      if (EnvironmentVariables.Any())
      {
        builder.AppendLine(2, "!include \"winmessages.nsh\"");
        builder.AppendLine(2, "!define env_hkcu 'HKCU \"Environment\"'");
      }

      if (Context == ShellVarContext.AllUsers)
      {
        builder.AppendLine(2, "SetShellVarContext {0}", Context == ShellVarContext.AllUsers ? "all" : "current");
      }

      if (InputDirectories.Any() || InputFileGroups.Any())
      {
        builder.AppendLine(2, "SetOutPath \"{0}\"", OutDir);
      }
  
      var overwrite = OverwriteMode.On;

      foreach (var directory in InputDirectories)
      {
        builder.AppendLine();
        overwrite = directory.AppendInstall(builder, overwrite);
      }

      if (InputFileGroups.Any())
      {
        builder.AppendLine();
      }

      foreach (var fileGroup in InputFileGroups)
      {
        overwrite = fileGroup.AppendInstall(builder, overwrite);
      }

      // TODO: Add code for InputFiles here

      if (Directories.Any())
      {
        builder.AppendLine();
      }

      foreach (var directory in Directories)
      {
        directory.AppendInstall(builder);
      }

      if (Directories.Any())
      {
        builder.AppendLine();
      }

      if (EnvironmentVariables.Any())
      {
        builder.AppendLine();
      }

      foreach (var environmentVariable in EnvironmentVariables)
      {
        environmentVariable.AppendInstall(builder);
      }

      builder.AppendLine("SectionEnd");
    }

    public virtual void AppendUninstall(StringBuilder builder, ShellVarContext currentContext)
    {
      if (currentContext != Context && (Context == ShellVarContext.AllUsers || currentContext == ShellVarContext.AllUsers))
      {
        builder.AppendLine(2, "SetShellVarContext {0}", Context == ShellVarContext.AllUsers ? "all" : "current");
      }

      foreach (var directory in Directories)
      {
        directory.AppendUninstall(builder);
      }

      foreach (var file in InputFileGroups)
      {
        file.AppendUninstall(builder, OutDir);
      }

      foreach (var directory in InputDirectories)
      {
        directory.AppendUninstall(builder, OutDir);
      }

      if (EnvironmentVariables.Any())
      {
        builder.AppendLine();
      }

      foreach (var environmentVariable in EnvironmentVariables)
      {
        environmentVariable.AppendUninstall(builder);
      }
    }

    public IEnumerable<string> GetPathsToRemove()
    {
      var pathsToRemove = new List<string>();
      pathsToRemove.AddRange(GetPaths(Directories.Select(d => d.Path)));
      pathsToRemove.AddRange(GetPaths(from file in InputFileGroups.SelectMany(g => g.Files)
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
