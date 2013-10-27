using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator.Builder
{
  public class FileGroupBuilder<TParent>
  {
    private FileGroup fileGroup;
    private List<FileBuilder<FileGroupBuilder<TParent>>> fileBuilders;

    internal FileGroupBuilder(FileGroup fileGroup, TParent parent)
    {
      this.fileGroup = fileGroup;
      fileBuilders = new List<FileBuilder<FileGroupBuilder<TParent>>>();
    }

    public TParent Parent { get; private set; }

    public FileGroupBuilder<TParent> SetOverwriteMode(OverwriteMode mode)
    {
      fileGroup.Overwrite = mode;
      return this;
    }

    public FileBuilder<FileGroupBuilder<TParent>> AddFile(string filePath)
    {
      var file = new File();
      file.FilePath = filePath;
      fileGroup.Files.Add(file);
      fileBuilders.Add(new FileBuilder<FileGroupBuilder<TParent>>(file, this));
      return fileBuilders.Last();
    }
  }
}
