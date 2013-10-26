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
    private List<AdditionalDirectoryBuilder> directoryBuilders;
    private List<FileGroupBuilder> fileGroupBuilders;

    public AdditionalSectionBuilder(string sectionName)
      : this(sectionName, null)
    {
    }

    internal AdditionalSectionBuilder(string sectionName, ScriptBuilder parent)
    {
      section = new FileBasedSection();
      directoryBuilders = new List<AdditionalDirectoryBuilder>();
      fileGroupBuilders = new List<FileGroupBuilder>();
      this.Parent = parent;
    }

    public ScriptBuilder Parent { get; private set; }

    #region SectionBase stuff

    public AdditionalSectionBuilder SetName(string name)
    {
      section.Name = name;
      return this;
    }

    public AdditionalSectionBuilder SetOutDir(string outDir)
    {
      section.OutDir = outDir;
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

    #region FileBasedSection stuff

    public AdditionalDirectoryBuilder AddDirectory(string directoryName)
    {
      directoryBuilders.Add(new AdditionalDirectoryBuilder(directoryName, this));
      return directoryBuilders.Last();
    }

    public FileGroupBuilder AddFileGroup()
    {
      fileGroupBuilders.Add(new FileGroupBuilder());
      return fileGroupBuilders.Last();
    }

    public AdditionalSectionBuilder AddShortCut(string shortCutPath, string targetPath)
    {
      section.ShortCuts.Add(new ShortCut()
                            {
                              ShortCutPath = shortCutPath,
                              TargetPath = targetPath
                            });
      return this;
    }

    public AdditionalSectionBuilder AddShortCut(string shortCutPath, string targetPath, string argument)
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

    public void Save(string fileName)
    {
      Serializer.Save(this, fileName);
    }
  }
}
