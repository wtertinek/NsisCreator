using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator
{
  [DebuggerDisplay("{Name}")]
  public class MainSection : FileBasedSection
  {
    private string productPublisher;
    private string productName;

    public MainSection()
    {
      Name = "MainSection";
      ExecutableName = "";
    }

    public string ExecutableName { get; set; }

    public void Init(string productPublisher, string productName)
    {
      this.productPublisher = productPublisher;
      this.productName = productName;
    }

    protected override void AppendInstallBodyEnd(StringBuilder builder)
    {
      base.AppendInstallBodyEnd(builder);

      builder.AppendLine();
      builder.AppendLine(2, "WriteUninstaller \"$INSTDIR\\uninst.exe\"");
      builder.AppendLine(2, "WriteRegStr HKLM \"${{PRODUCT_DIR_REGKEY}}\" \"\" \"$INSTDIR\\{0}\"", ExecutableName);
      builder.AppendLine(2, "WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} \"${PRODUCT_UNINST_KEY}\" \"DisplayName\" \"$(^Name)\"");
      builder.AppendLine(2, "WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} \"${PRODUCT_UNINST_KEY}\" \"UninstallString\" \"$INSTDIR\\uninst.exe\"");
      builder.AppendLine(2, "WriteRegStr ${{PRODUCT_UNINST_ROOT_KEY}} \"${{PRODUCT_UNINST_KEY}}\" \"DisplayIcon\" \"$INSTDIR\\{0}\"", ExecutableName);
      builder.AppendLine(2, "WriteRegStr ${{PRODUCT_UNINST_ROOT_KEY}} \"${{PRODUCT_UNINST_KEY}}\" \"DisplayVersion\" \"{0}\"", productPublisher);
      builder.AppendLine(2, "WriteRegStr ${{PRODUCT_UNINST_ROOT_KEY}} \"${{PRODUCT_UNINST_KEY}}\" \"Publisher\" \"{0}\"", productPublisher);
    }

    protected override void AppendUninstallBodyMain(StringBuilder builder)
    {
      builder.AppendLine(2, "Delete \"$INSTDIR\\uninst.exe\"");
      base.AppendUninstallBodyMain(builder);
    }

    protected override void AppendUninstallBodyEnd(StringBuilder builder)
    {
      builder.AppendLine();
      builder.AppendLine(2, "DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} \"${PRODUCT_UNINST_KEY}\"");
      builder.AppendLine(2, "DeleteRegKey HKLM \"${PRODUCT_DIR_REGKEY}\"");

      base.AppendUninstallBodyEnd(builder);
    }
  }
}
