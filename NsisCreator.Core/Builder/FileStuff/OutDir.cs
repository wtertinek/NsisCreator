using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator.Builder
{
  public class OutDir
  {
    private OutDir(string path)
    {
      CheckDirectory(path);
      Path = path;
    }

    private OutDir(string root, string mainDirectory, params string[] subDirectories)
    {
      if (!string.IsNullOrEmpty(mainDirectory))
      {
        CheckDirectory(mainDirectory);
      }

      subDirectories.ToList()
                    .ForEach(d => CheckDirectory(d));

      var builder = new StringBuilder();
      builder.Append(root);
      builder.Append("\\");
      builder.Append(mainDirectory);

      foreach (var dir in subDirectories)
      {
        builder.Append("\\");
        builder.Append(dir);
      }

      Path = builder.ToString();
    }

    internal string Path { get; private set; }

    private void CheckDirectory(string directory)
    {
      var chars = System.IO.Path.GetInvalidPathChars();
      
      if (directory.Any(c => chars.Contains(c)))
      {
        throw new Exception("Directory name " + directory + " is not valid.");
      }
    }

    public static OutDir FullPath(string fullPath)
    {
      return new OutDir(fullPath);
    }

    public static OutDir ProgramFiles(string mainDirectory, params string[] subDirectories)
    {
      return new OutDir("$PROGRAMFILES", mainDirectory, subDirectories);
    }

    public static OutDir CommonFiles(string mainDirectory, params string[] subDirectories)
    {
      return new OutDir("$COMMONFILES", mainDirectory, subDirectories);
    }

    public static OutDir AppData(string mainDirectory, params string[] subDirectories)
    {
      return new OutDir("$APPDATA", mainDirectory, subDirectories);
    }

    public static OutDir LocalAppData(string mainDirectory, params string[] subDirectories)
    {
      return new OutDir("$LOCALAPPDATA", mainDirectory, subDirectories);
    }

    public static OutDir Profile(string mainDirectory, params string[] subDirectories)
    {
      return new OutDir("$PROFILE", mainDirectory, subDirectories);
    }

    public static OutDir Desktop(string mainDirectory, params string[] subDirectories)
    {
      return new OutDir("$DESKTOP", mainDirectory, subDirectories);
    }

    public static OutDir Temp(string mainDirectory, params string[] subDirectories)
    {
      return new OutDir("$TEMP", mainDirectory, subDirectories);
    }

    public static OutDir WinDir()
    {
      return new OutDir("$WINDIR", "");
    }

    public static OutDir SysDir()
    {
      return new OutDir("$SYSDIR", "");
    }
  }
}
