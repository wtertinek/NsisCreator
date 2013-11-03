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

    private Directory(string root, params string[] subDirectories)
    {
      subDirectories.ToList()
                    .ForEach(d => CheckDirectory(d));

      var builder = new StringBuilder();
      builder.Append(root);

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

    public static Directory InstallDir(params string[] subDirectories)
    {
      return new Directory("$INSTDIR", subDirectories);
    }

    public static Directory ProgramFiles(params string[] subDirectories)
    {
      return new Directory("$PROGRAMFILES", subDirectories);
    }

    public static Directory CommonFiles(params string[] subDirectories)
    {
      return new Directory("$COMMONFILES", subDirectories);
    }

    public static Directory AppData(params string[] subDirectories)
    {
      return new Directory("$APPDATA", subDirectories);
    }

    public static Directory LocalAppData(params string[] subDirectories)
    {
      return new Directory("$LOCALAPPDATA", subDirectories);
    }

    public static Directory UserProfile(params string[] subDirectories)
    {
      return new Directory("$PROFILE", subDirectories);
    }

    public static Directory Desktop(params string[] subDirectories)
    {
      return new Directory("$DESKTOP", subDirectories);
    }

    public static Directory StartMenu()
    {
      return new Directory("$STARTMENU", "");
    }

    public static Directory StartMenuPrograms(params string[] subDirectories)
    {
      return new Directory("$SMPROGRAMS", subDirectories);
    }

    public static Directory Temp(params string[] subDirectories)
    {
      return new Directory("$TEMP", subDirectories);
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
