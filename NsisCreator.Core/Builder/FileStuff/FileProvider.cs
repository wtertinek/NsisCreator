using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator.Builder
{
  public class FileProvider<TParent>
  {
    private FileBasedSection section;
    private List<DirectoryBuilder<FileProvider<TParent>>> directoryBuilders;
    private List<FileGroupBuilder<FileProvider<TParent>>> fileGroupBuilders;
    private List<FileBuilder<FileProvider<TParent>>> fileBuilders;

    internal FileProvider(FileBasedSection section, TParent parent)
    {
      this.section = section;
      this.Parent = parent;
      directoryBuilders = new List<DirectoryBuilder<FileProvider<TParent>>>();
      fileGroupBuilders = new List<FileGroupBuilder<FileProvider<TParent>>>();
      fileBuilders = new List<FileBuilder<FileProvider<TParent>>>();
    }

    public TParent Parent { get; private set; }

    public DirectoryBuilder<FileProvider<TParent>> AddFromDirectory(string directoryName)
    {
      var directory = new Directory();
      directory.DirectoryName = directoryName;
      section.Directories.Add(directory);
      directoryBuilders.Add(new DirectoryBuilder<FileProvider<TParent>>(directory, this));
      return directoryBuilders.Last();
    }

    public FileGroupBuilder<FileProvider<TParent>> AddFileGroup()
    {
      var fileGroup = new FileGroup();
      section.FileGroups.Add(fileGroup);
      fileGroupBuilders.Add(new FileGroupBuilder<FileProvider<TParent>>(fileGroup, this));
      return fileGroupBuilders.Last();
    }

    public FileBuilder<FileProvider<TParent>> AddFile(string filePath)
    {
      var file = new File();
      file.FilePath = filePath;
      section.Files.Add(file);
      fileBuilders.Add(new FileBuilder<FileProvider<TParent>>(file, this));
      return fileBuilders.Last();
    }
  }
}
