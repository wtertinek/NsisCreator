using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator
{
  public abstract class SectionBase
  {
    protected SectionBase()
    {
      Name = "";
      OutDir = "$INSTDIR";
      Context = ShellVarContext.CurrentUser;
      EnvironmentVariables = new List<EnvironmentVariable>();
    }

    public string Name { get; set; }

    public string OutDir { get; set; }

    public ShellVarContext Context { get; set; }

    public List<EnvironmentVariable> EnvironmentVariables { get; set; }

    public virtual void AppendInstall(StringBuilder builder, int index)
    {
      AppendInstallHeader(builder, index);
      AppendInstallBodyBegin(builder);
      AppendInstallBodyMain(builder);
      AppendInstallBodyEnd(builder);
      AppendInstallFooter(builder);
    }

    protected void AppendInstallHeader(StringBuilder builder, int index)
    {
      builder.AppendLine("Section \"{0}\" SEC{1}", Name, index.ToString("D2"));
    }

    protected virtual void AppendInstallBodyBegin(StringBuilder builder)
    {
      if (EnvironmentVariables.Any())
      {
        builder.AppendLine(2, "!include \"winmessages.nsh\"");
        builder.AppendLine(2, "!define env_hkcu 'HKCU \"Environment\"'");
      }

      if (Context == ShellVarContext.AllUsers)
      {
        builder.AppendLine(2, "SetShellVarContext {0}", Context == ShellVarContext.AllUsers ? "all" : "current");
      }
    }

    protected virtual void AppendInstallBodyMain(StringBuilder builder)
    {
      if (EnvironmentVariables.Any())
      {
        builder.AppendLine();
      }

      foreach (var environmentVariable in EnvironmentVariables)
      {
        environmentVariable.AppendInstall(builder);
      }
    }

    protected virtual void AppendInstallBodyEnd(StringBuilder builder)
    {

    }

    protected void AppendInstallFooter(StringBuilder builder)
    {
      builder.AppendLine("SectionEnd");
    }

    public virtual void AppendUninstall(StringBuilder builder, ShellVarContext lastContext)
    {
      AppendUninstallBodyBegin(builder, lastContext);
      AppendUninstallBodyMain(builder);
      AppendUninstallBodyEnd(builder);
    }

    protected virtual void AppendUninstallBodyBegin(StringBuilder builder, ShellVarContext currentContext)
    {
      if (currentContext != Context && (Context == ShellVarContext.AllUsers || currentContext == ShellVarContext.AllUsers))
      {
        builder.AppendLine(2, "SetShellVarContext {0}", Context == ShellVarContext.AllUsers ? "all" : "current");
      }
    }

    protected virtual void AppendUninstallBodyMain(StringBuilder builder)
    {
      if (EnvironmentVariables.Any())
      {
        builder.AppendLine();
      }

      foreach (var environmentVariable in EnvironmentVariables)
      {
        environmentVariable.AppendUninstall(builder);
      }
    }

    protected virtual void AppendUninstallBodyEnd(StringBuilder builder)
    {
      
    }
  }
}
