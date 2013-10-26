using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator.Builder
{
  public class FileBuilder
  {
    private File file;

    internal FileBuilder()
    {
      file = new File();
    }

    public FileBuilder SetSourceName(string filePath)
    {
      file.SourceName = filePath;
      return this;
    }

    public FileBuilder SetTargetName(string fileName)
    {
      file.TargetName = fileName;
      return this;
    }

    public FileBuilder KeepFileOnUninstall(bool keep)
    {
      file.KeepFileOnUninstall = keep;
      return this;
    }
  }
}
