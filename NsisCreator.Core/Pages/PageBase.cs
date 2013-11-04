using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator
{
  public abstract class PageBase
  {
    private string instMacro;
    private string uninstMacro;

    protected PageBase(PageType pageType)
    {
      var name = pageType.ToString().ToUpper();
      instMacro = "!insertmacro MUI_PAGE_" + name;
      uninstMacro = "!insertmacro MUI_UNPAGE_" + name;
    }

    public void AppendInstallMacro(StringBuilder builder)
    {
      builder.AppendLine(instMacro);
    }

    public void AppendUninstallMacro(StringBuilder builder)
    {
      builder.AppendLine(uninstMacro);
    }

    protected enum PageType
    {
      Welcome,
      License,
      Components,
      Directory,
      Startmenu,
      InstFiles,
      Finish
    }
  }
}
