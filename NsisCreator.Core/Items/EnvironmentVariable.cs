using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator
{
  public class EnvironmentVariable
  {
    public EnvironmentVariable()
    {
      Name = "";
      Value = "";
    }

    public string Name { get; set; }

    public string Value { get; set; }

    public void AppendInstall(StringBuilder builder)
    {
      builder.AppendLine(2, "WriteRegExpandStr ${env_hkcu} {0} \"{1}\"", Name, Value);
      builder.AppendLine(2, "SendMessage ${HWND_BROADCAST} ${WM_WININICHANGE} 0 \"STR:Environment\" /TIMEOUT=5000");      
    }

    public void AppendUninstall(StringBuilder builder)
    {
      builder.AppendLine(2, "DeleteRegValue ${{env_hkcu}} {0}", Name);
      builder.AppendLine(2, "SendMessage ${{HWND_BROADCAST}} ${{WM_WININICHANGE}} 0 \"STR:Environment\" /TIMEOUT=5000");
    }
  }
}
