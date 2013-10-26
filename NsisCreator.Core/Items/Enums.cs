using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator
{
  public enum OverwriteMode
  {
    On,
    Off,
    Try,
    IfNewer,
    IfDiff,
    LastUsed
  }

  public enum ShellVarContext
  {
    AllUsers,
    CurrentUser
  }
}
