using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator.Builder
{
  public class DirectoryBuilder<TParent>
  {
    private AdditionalDirectory directory;

    public DirectoryBuilder(AdditionalDirectory directory, TParent parent)
    {
      this.directory = directory;
      Parent = parent;
    }

    public TParent Parent { get; private set; }

    public DirectoryBuilder<TParent> AddShortCut(string name, string targetPath)
    {
      return AddShortCut(name, targetPath, "");
    }

    public DirectoryBuilder<TParent> AddShortCut(string name, string targetPath, string argument)
    {
      directory.ShortCuts.Add(new ShortCut()
                              {
                                Path = System.IO.Path.Combine(directory.Path, name),
                                TargetPath = targetPath,
                                Argument = argument
                              });
      return this;
    }

    public DirectoryBuilder<TParent> AddShortCutToUninstall(string name)
    {
      return AddShortCut(name, @"$INSTDIR\uninst.exe", "");
    }
  }
}
