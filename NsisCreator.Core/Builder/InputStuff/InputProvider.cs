using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator.Builder
{
  public class InputProvider<TParent>
  {
    private Section section;
    private List<InputDirectoryBuilder<InputProvider<TParent>>> directoryBuilders;
    private List<InputFileGroupBuilder<InputProvider<TParent>>> fileGroupBuilders;
    private List<InputFileBuilder<InputProvider<TParent>>> fileBuilders;

    internal InputProvider(Section section, TParent parent)
    {
      this.section = section;
      this.Parent = parent;
      directoryBuilders = new List<InputDirectoryBuilder<InputProvider<TParent>>>();
      fileGroupBuilders = new List<InputFileGroupBuilder<InputProvider<TParent>>>();
      fileBuilders = new List<InputFileBuilder<InputProvider<TParent>>>();
    }

    public TParent Parent { get; private set; }

    public InputDirectoryBuilder<InputProvider<TParent>> AddFromDirectory(string directoryName)
    {
      var directory = new InputDirectory();
      directory.DirectoryName = directoryName;
      section.InputDirectories.Add(directory);
      directoryBuilders.Add(new InputDirectoryBuilder<InputProvider<TParent>>(directory, this));
      return directoryBuilders.Last();
    }

    public InputFileGroupBuilder<InputProvider<TParent>> AddFileGroup()
    {
      var fileGroup = new InputFileGroup();
      section.InputFileGroups.Add(fileGroup);
      fileGroupBuilders.Add(new InputFileGroupBuilder<InputProvider<TParent>>(fileGroup, this));
      return fileGroupBuilders.Last();
    }

    public InputFileBuilder<InputProvider<TParent>> AddFile(string filePath)
    {
      var file = new InputFile();
      file.FilePath = filePath;
      section.InputFiles.Add(file);
      fileBuilders.Add(new InputFileBuilder<InputProvider<TParent>>(file, this));
      return fileBuilders.Last();
    }
  }
}
