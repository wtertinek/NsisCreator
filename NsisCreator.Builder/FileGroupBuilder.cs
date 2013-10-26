using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator.Builder
{
  public class FileGroupBuilder
  {
    private FileGroup fileGroup;
    private List<FileBuilder> fileBuilders;

    internal FileGroupBuilder()
    {
      fileGroup = new FileGroup();
      fileBuilders = new List<FileBuilder>();
    }

    public FileGroupBuilder SetOverwriteMode(OverwriteMode mode)
    {
      fileGroup.Overwrite = mode;
      return this;
    }

    public FileBuilder AddFile()
    {
      fileBuilders.Add(new FileBuilder());
      return fileBuilders.Last();
    }
  }
}
