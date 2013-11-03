using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator
{
  public class Uninstaller
  {
    public Uninstaller()
    {
    }

    public void AppendInstall(StringBuilder builder, string productPublisher, string productVersion, string executableName)
    {
      builder.AppendLine("Section -Uninstaller");
      builder.AppendLine(2, "WriteUninstaller \"$INSTDIR\\uninst.exe\"");
      builder.AppendLine(2, "WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} \"${PRODUCT_UNINST_KEY}\" \"DisplayName\" \"$(^Name)\"");
      builder.AppendLine(2, "WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} \"${PRODUCT_UNINST_KEY}\" \"UninstallString\" \"$INSTDIR\\uninst.exe\"");
      builder.AppendLine(2, "WriteRegStr ${{PRODUCT_UNINST_ROOT_KEY}} \"${{PRODUCT_UNINST_KEY}}\" \"DisplayIcon\" \"$INSTDIR\\{0}\"", executableName);
      builder.AppendLine(2, "WriteRegStr ${{PRODUCT_UNINST_ROOT_KEY}} \"${{PRODUCT_UNINST_KEY}}\" \"DisplayVersion\" \"{0}\"", productVersion);
      builder.AppendLine(2, "WriteRegStr ${{PRODUCT_UNINST_ROOT_KEY}} \"${{PRODUCT_UNINST_KEY}}\" \"Publisher\" \"{0}\"", productPublisher);
      builder.AppendLine("SectionEnd");
    }

    public void AppendUninstall(StringBuilder builder)
    {
      builder.AppendLine(2, "Delete \"$INSTDIR\\uninst.exe\"");
      builder.AppendLine(2, "DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} \"${PRODUCT_UNINST_KEY}\"");
      builder.AppendLine(2, "DeleteRegKey HKLM \"${PRODUCT_DIR_REGKEY}\"");
    }
  }
}
