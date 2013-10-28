using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator.Builder
{
  public class Directory
  {
    private Directory(string path)
    {
      CheckDirectory(path);
      Path = path;
    }

    private Directory(string root, string mainDirectory, params string[] subDirectories)
    {
      if (!string.IsNullOrEmpty(mainDirectory))
      {
        CheckDirectory(mainDirectory);
      }

      subDirectories.ToList()
                    .ForEach(d => CheckDirectory(d));

      var builder = new StringBuilder();
      builder.Append(root);

      if (!string.IsNullOrEmpty(mainDirectory))
      {
        builder.Append("\\");
        builder.Append(mainDirectory);
      }

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

    public static Directory FullPath(string fullPath)
    {
      return new Directory(fullPath);
    }

    public static Directory ProgramFiles(string mainDirectory, params string[] subDirectories)
    {
      return new Directory("$PROGRAMFILES", mainDirectory, subDirectories);
    }

    public static Directory CommonFiles(string mainDirectory, params string[] subDirectories)
    {
      return new Directory("$COMMONFILES", mainDirectory, subDirectories);
    }

    public static Directory AppData(string mainDirectory, params string[] subDirectories)
    {
      return new Directory("$APPDATA", mainDirectory, subDirectories);
    }

    public static Directory LocalAppData(string mainDirectory, params string[] subDirectories)
    {
      return new Directory("$LOCALAPPDATA", mainDirectory, subDirectories);
    }

    public static Directory UserProfile(string mainDirectory, params string[] subDirectories)
    {
      return new Directory("$PROFILE", mainDirectory, subDirectories);
    }

    public static Directory Desktop(string mainDirectory, params string[] subDirectories)
    {
      return new Directory("$DESKTOP", mainDirectory, subDirectories);
    }

    public static Directory StartMenu()
    {
      return new Directory("$STARTMENU", "");
    }

    public static Directory StartMenuPrograms(string mainDirectory, params string[] subDirectories)
    {
      return new Directory("$SMPROGRAMS", mainDirectory, subDirectories);
    }

    public static Directory Temp(string mainDirectory, params string[] subDirectories)
    {
      return new Directory("$TEMP", mainDirectory, subDirectories);
    }

    public static Directory WinDir()
    {
      return new Directory("$WINDIR", "");
    }

    public static Directory SysDir()
    {
      return new Directory("$SYSDIR", "");
    }
  }
}
