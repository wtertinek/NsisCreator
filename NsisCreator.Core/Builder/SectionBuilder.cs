using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator.Builder
{
  public class SectionBuilder
  {
    private Section section;
    private List<DirectoryBuilder<SectionBuilder>> directories;

    internal SectionBuilder(Section section, ScriptBuilder parent)
    {
      this.section = section;
      Parent = parent;
      Files = new InputProvider<SectionBuilder>(section, this);
      directories = new List<DirectoryBuilder<SectionBuilder>>();
    }

    public ScriptBuilder Parent { get; private set; }

    public InputProvider<SectionBuilder> Files { get; private set; }
    
    #region SectionBase stuff

    public SectionBuilder SetName(string name)
    {
      section.Name = name;
      return this;
    }

    public SectionBuilder SetOutDir(Directory outDir)
    {
      section.OutDir = outDir.Path;
      return this;
    }

    public SectionBuilder SetShellVarContextAllUsers()
    {
      section.Context = ShellVarContext.AllUsers;
      return this;
    }

    public SectionBuilder SetShellVarContextCurrentUser()
    {
      section.Context = ShellVarContext.CurrentUser;
      return this;
    }

    public SectionBuilder AddEnvironmentVariable(string name, string value)
    {
      section.EnvironmentVariables.Add(new EnvironmentVariable()
                                       {
                                         Name = name,
                                         Value = value
                                       });
      return this;
    }

    #endregion

    public void Save(string fileName)
    {
      Serializer.Save(this, fileName);
    }

    public DirectoryBuilder<SectionBuilder> Create(Directory directory)
    {
      var dir = new AdditionalDirectory() { Path = directory.Path };
      section.Directories.Add(dir);
      directories.Add(new DirectoryBuilder<SectionBuilder>(dir, this));
      return directories.Last();
    }
  }
}
