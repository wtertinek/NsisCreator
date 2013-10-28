using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator.Builder
{
  public class InputFileGroupBuilder<TParent>
  {
    private InputFileGroup fileGroup;
    private List<InputFileBuilder<InputFileGroupBuilder<TParent>>> fileBuilders;

    internal InputFileGroupBuilder(InputFileGroup fileGroup, TParent parent)
    {
      this.fileGroup = fileGroup;
      fileBuilders = new List<InputFileBuilder<InputFileGroupBuilder<TParent>>>();
    }

    public TParent Parent { get; private set; }

    public InputFileGroupBuilder<TParent> SetOverwriteMode(OverwriteMode mode)
    {
      fileGroup.Overwrite = mode;
      return this;
    }

    public InputFileBuilder<InputFileGroupBuilder<TParent>> AddFile(string filePath)
    {
      var file = new InputFile();
      file.FilePath = filePath;
      fileGroup.Files.Add(file);
      fileBuilders.Add(new InputFileBuilder<InputFileGroupBuilder<TParent>>(file, this));
      return fileBuilders.Last();
    }
  }
}
