using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator.Builder
{
  public class AdditionalSectionBuilder
  {
    private FileBasedSection section;

    public AdditionalSectionBuilder(string sectionName)
      : this(new FileBasedSection() { Name = sectionName }, null)
    {
    }

    internal AdditionalSectionBuilder(FileBasedSection section, ScriptBuilder parent)
    {
      Parent = parent;
      this.section = section;
      Files = new InputProvider<AdditionalSectionBuilder>(section, this);
    }

    public ScriptBuilder Parent { get; private set; }

    public InputProvider<AdditionalSectionBuilder> Files { get; private set; }

    #region SectionBase stuff

    public AdditionalSectionBuilder SetName(string name)
    {
      section.Name = name;
      return this;
    }

    public AdditionalSectionBuilder SetOutDir(Directory outDir)
    {
      section.OutDir = outDir.Path;
      return this;
    }

    public AdditionalSectionBuilder SetShellVarContextAllUsers()
    {
      section.Context = ShellVarContext.AllUsers;
      return this;
    }

    public AdditionalSectionBuilder SetShellVarContextCurrentUser()
    {
      section.Context = ShellVarContext.CurrentUser;
      return this;
    }

    public AdditionalSectionBuilder AddEnvironmentVariable(string name, string value)
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
  }
}
