using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator.Builder
{
  public class MainSectionBuilder
  {
    private MainSection section;
    private List<MainDirectoryBuilder> directoryBuilders;
    private List<FileGroupBuilder> fileGroupBuilders;

    internal MainSectionBuilder(ScriptBuilder parent)
    {
      section = new MainSection();
      directoryBuilders = new List<MainDirectoryBuilder>();
      fileGroupBuilders = new List<FileGroupBuilder>();
      Parent = parent;
    }

    public ScriptBuilder Parent { get; private set; }

    #region SectionBase stuff

    public MainSectionBuilder SetName(string name)
    {
      section.Name = name;
      return this;
    }

    public MainSectionBuilder SetOutDir(string outDir)
    {
      section.OutDir = outDir;
      return this;
    }

    public MainSectionBuilder SetShellVarContextAllUsers()
    {
      section.Context = ShellVarContext.AllUsers;
      return this;
    }

    public MainSectionBuilder SetShellVarContextCurrentUser()
    {
      section.Context = ShellVarContext.CurrentUser;
      return this;
    }

    public MainSectionBuilder AddEnvironmentVariable(string name, string value)
    {
      section.EnvironmentVariables.Add(new EnvironmentVariable()
                                       {
                                         Name = name,
                                         Value = value
                                       });
      return this;
    }

    #endregion

    #region FileBasedSection stuff

    public MainDirectoryBuilder AddDirectory(string directoryName)
    {
      directoryBuilders.Add(new MainDirectoryBuilder(directoryName, this));
      return directoryBuilders.Last();
    }

    public FileGroupBuilder AddFileGroup()
    {
      fileGroupBuilders.Add(new FileGroupBuilder());
      return fileGroupBuilders.Last();
    }

    public MainSectionBuilder AddShortCut(string shortCutPath, string targetPath)
    {
      section.ShortCuts.Add(new ShortCut()
                            {
                              ShortCutPath = shortCutPath,
                              TargetPath = targetPath
                            });
      return this;
    }

    public MainSectionBuilder AddShortCut(string shortCutPath, string targetPath, string argument)
    {
      section.ShortCuts.Add(new ShortCut()
                            {
                              ShortCutPath = shortCutPath,
                              TargetPath = targetPath,
                              Argument = argument
                            });
      return this;
    }

    #endregion

    public MainSectionBuilder SetExecutableName(string executableName)
    {
      section.ExecutableName = executableName;
      return this;
    }
  }
}
