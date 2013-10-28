using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator.Builder
{
  public class InputFileBuilder<TParent>
  {
    private InputFile file;

    internal InputFileBuilder(InputFile file, TParent parent)
    {
      this.file = file;
      Parent = parent;
    }

    public TParent Parent { get; private set; }

    public InputFileBuilder<TParent> SetTargetName(string fileName)
    {
      file.TargetName = fileName;
      return this;
    }

    public InputFileBuilder<TParent> KeepFileAfterUninstall(bool keep)
    {
      file.KeepFileAfterUninstall = keep;
      return this;
    }
  }
}
