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

    internal MainSectionBuilder(MainSection section, ScriptBuilder parent)
    {
      this.section = section;
      Parent = parent;
      Files = new FileProvider<MainSectionBuilder>(section, this);
    }

    public ScriptBuilder Parent { get; private set; }

    public FileProvider<MainSectionBuilder> Files { get; private set; }

    #region SectionBase stuff

    public MainSectionBuilder SetName(string name)
    {
      section.Name = name;
      return this;
    }

    public MainSectionBuilder SetOutDir(OutDir outDir)
    {
      section.OutDir = outDir.Path;
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

    public MainSectionBuilder SetExecutableName(string executableName)
    {
      section.ExecutableName = executableName;
      return this;
    }
  }
}
