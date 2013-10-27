using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator.Builder
{
  public class FileBuilder<TParent>
  {
    private File file;

    internal FileBuilder(File file, TParent parent)
    {
      this.file = file;
      Parent = parent;
    }

    public TParent Parent { get; private set; }

    public FileBuilder<TParent> SetTargetName(string fileName)
    {
      file.TargetName = fileName;
      return this;
    }

    public FileBuilder<TParent> KeepFileOnUninstall(bool keep)
    {
      file.KeepFileOnUninstall = keep;
      return this;
    }
  }
}
