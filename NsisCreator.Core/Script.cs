using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NsisCreator
{
  public class Script
  {
    public Script()
    {
      ProductName = "";
      ProductVersion = Builder.Variable.Create("PRODUCT_VERSION");
      ProductPublisher = "";
      OutFileName = Builder.Variable.Create("OUT_FILE");
      Sections = new List<Section>();
      Uninstaller = true;
    }

    public string ProductName { get; set; }
    
    public string ProductPublisher { get; set; }

    public string ProductVersion { get; set; }

    public string InstallDir { get; set; }

    public string ExecutableName { get; set; }

    public string OutFileName { get; set; }

    public bool Uninstaller { get; set; }

    public bool ShowDetails { get; set; }

    public bool AllowSilentInstall { get; set; }

    public List<Section> Sections { get; set; }

    public string Generate()
    {
      var builder = new StringBuilder();
      builder.AppendLine("!define PRODUCT_DIR_REGKEY \"Software\\Microsoft\\Windows\\CurrentVersion\\App Paths\\{0}\"", ExecutableName);
      builder.AppendLine("!define PRODUCT_UNINST_KEY \"Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{0}\"", ProductName);
      builder.AppendLine("!define PRODUCT_UNINST_ROOT_KEY \"HKLM\"");
      builder.AppendLine();
      // TODO: Currently this code is static, insert page as a type.
      builder.AppendLine("; MUI 1.67 compatible ------");
      builder.AppendLine("!include \"MUI.nsh\"");
      builder.AppendLine();
      builder.AppendLine("; MUI Settings");
      builder.AppendLine("!define MUI_ABORTWARNING");
      builder.AppendLine("!define MUI_ICON \"${NSISDIR}\\Contrib\\Graphics\\Icons\\modern-install.ico\"");
      builder.AppendLine("!define MUI_UNICON \"${NSISDIR}\\Contrib\\Graphics\\Icons\\modern-uninstall.ico\"");
      builder.AppendLine();
      builder.AppendLine("; Welcome page");
      builder.AppendLine("!insertmacro MUI_PAGE_WELCOME");
      builder.AppendLine("; Instfiles page");
      builder.AppendLine("!insertmacro MUI_PAGE_INSTFILES");
      builder.AppendLine("; Finish page");
      builder.AppendLine("!insertmacro MUI_PAGE_FINISH");
      builder.AppendLine("; Uninstaller pages");
      builder.AppendLine("!insertmacro MUI_UNPAGE_INSTFILES");
      builder.AppendLine();
      builder.AppendLine("; Language files");
      builder.AppendLine("!insertmacro MUI_LANGUAGE \"German\"");
      builder.AppendLine();
      builder.AppendLine("; MUI end ------");
      builder.AppendLine();
      builder.AppendLine("Name \"{0}\"", ProductName);
      builder.AppendLine("OutFile \"{0}\"", OutFileName);
      builder.AppendLine("InstallDir \"{0}\"", InstallDir);
      builder.AppendLine("InstallDirRegKey HKLM \"${PRODUCT_DIR_REGKEY}\" \"\"");
      builder.AppendLine("ShowInstDetails {0}", ShowDetails ? "show" : "hide");
      builder.AppendLine("ShowUnInstDetails {0}", ShowDetails ? "show" : "hide");
      builder.AppendLine("SilentInstall {0}", AllowSilentInstall ? "silent" : "normal");
      builder.AppendLine("SilentUnInstall {0}", AllowSilentInstall ? "silent" : "normal");

      for (int i = 0; i < Sections.Count; i++)
      {
        builder.AppendLine();
        Sections[i].AppendInstall(builder, i + 1);
      }

      if (Uninstaller)
      {
        builder.AppendLine();

        var uninstaller = new NsisCreator.Uninstaller();
        uninstaller.AppendInstall(builder, ProductPublisher, ProductName, ExecutableName);
      }

      // TODO: Remove german texts

      builder.AppendLine();
      builder.AppendLine("Function un.onUninstSuccess");      
      builder.AppendLine(2, "HideWindow");
      builder.AppendLine(2, "MessageBox MB_ICONINFORMATION|MB_OK \"$(^Name) wurde erfolgreich vom Computer entfernt.\" /SD IDOK");
      builder.AppendLine("FunctionEnd");
      builder.AppendLine();
      builder.AppendLine("Function un.onInit");
      builder.AppendLine(2, "MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 \"Sind sie sicher, dass sie $(^Name) und alle zugehörigen Komponenten entfernen möchten?\" /SD IDYES IDYES +2");
      builder.AppendLine(2, "Abort");
      builder.AppendLine("FunctionEnd");
      builder.AppendLine();
      builder.AppendLine("# Function taken from http://nsis.sourceforge.net/Check_if_dir_is_empty");
      builder.AppendLine("Function un.isEmptyDir");
      builder.AppendLine(2, "# Stack ->                    # Stack: <directory>");
      builder.AppendLine(2, "Exch $0                       # Stack: $0");
      builder.AppendLine(2, "Push $1                       # Stack: $1, $0");
      builder.AppendLine(2, "FindFirst $0 $1 \"$0\\*.*\"");
      builder.AppendLine(2, "strcmp $1 \".\" 0 _notempty");
      builder.AppendLine(4, "FindNext $0 $1");
      builder.AppendLine(4, "strcmp $1 \"..\" 0 _notempty");
      builder.AppendLine(6, "ClearErrors");
      builder.AppendLine(6, "FindNext $0 $1");
      builder.AppendLine(6, "IfErrors 0 _notempty");
      builder.AppendLine(8, "FindClose $0");
      builder.AppendLine(8, "Pop $1                  # Stack: $0");
      builder.AppendLine(8, "StrCpy $0 1");
      builder.AppendLine(8, "Exch $0                 # Stack: 1 (true)");
      builder.AppendLine(8, "goto _end");
      builder.AppendLine(6, "_notempty:");
      builder.AppendLine(8, "FindClose $0");
      builder.AppendLine(8, "ClearErrors");
      builder.AppendLine(8, "Pop $1                   # Stack: $0");
      builder.AppendLine(8, "StrCpy $0 0");
      builder.AppendLine(8, "Exch $0                  # Stack: 0 (false)");
      builder.AppendLine(2, "_end:");
      builder.AppendLine("FunctionEnd");
      builder.AppendLine();
      builder.AppendLine("Section Uninstall");

      if (Sections.Any(s => s.EnvironmentVariables.Any()))
      {
        builder.AppendLine(2, "!include \"winmessages.nsh\"");
        builder.AppendLine(2, "!define env_hkcu 'HKCU \"Environment\"'");
        builder.AppendLine();
      }

      var lastContext = ShellVarContext.CurrentUser;

      for (int i = Sections.Count - 1; i >= 0; i--)
      {
        Sections[i].AppendUninstall(builder, lastContext);
        lastContext = Sections[i].Context;

        if (Sections[i].OutDir == "$INSTDIR")
        {
          Sections[i].OutDir = InstallDir;
        }

        if (i > 0)
        {
          builder.AppendLine();
        }
      }

      var pathsToRemove = Sections.SelectMany(s => s.GetPathsToRemove())
                                  .Distinct()
                                  .Where(p => p.Contains("\\"))
                                  .GroupBy(g => g.Split('\\')[0])
                                  .OrderByDescending(g => g.Key)
                                  .SelectMany(g => g.OrderByDescending(p => p))
                                  .ToArray();
        
      foreach (var path in pathsToRemove)
      {
        builder.AppendLine();
        builder.AppendLine(2, "Push \"{0}\"", path);
        builder.AppendLine(2, "Call un.isEmptyDir");
        builder.AppendLine(2, "Pop $0");
        builder.AppendLine(2, "StrCmp $0 1 0 +2");
        builder.AppendLine(4, "RMDir \"{0}\"", path);
      }

      builder.AppendLine();
      builder.AppendLine(2, "SetAutoClose true");
      builder.AppendLine("SectionEnd");

      return builder.ToString();
    }
  }
}
