using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator.Builder
{
  public class ScriptBuilder
  {
    private Script script;
    private List<SectionBuilder> sections;

    public ScriptBuilder()
    {
      script = new Script();
      sections = new List<SectionBuilder>();
    }

    public SectionBuilder AddAdditionalSection(string sectionName)
    {
      var section = new Section() { Name = sectionName };
      script.Sections.Add(section);
      sections.Add(new SectionBuilder(section, this));
      return sections.Last();
    }

    public ScriptBuilder SetProductName(string productName)
    {
      script.ProductName = productName;
      return this;
    }

    public ScriptBuilder SetProductVersion(string productVersion)
    {
      script.ProductVersion = productVersion;
      return this;
    }

    public ScriptBuilder SetProductPublisher(string productPublisher)
    {
      script.ProductPublisher = productPublisher;
      return this;
    }

    public ScriptBuilder SetExecutableName(string executableName)
    {
      script.ExecutableName = executableName;
      return this;
    }

    public ScriptBuilder SetInstallDir(string installDir)
    {
      script.InstallDir = installDir;
      return this;
    }

    public ScriptBuilder SetOutFileName(string outFileName)
    {
      script.OutFileName = outFileName;
      return this;
    }

    public ScriptBuilder SetLicenseFileName(string licenseFileName)
    {
      script.LicenseFileName = licenseFileName;
      return this;
    }

    public ScriptBuilder ShowDetails(bool show)
    {
      script.ShowDetails = show;
      return this;
    }

    public ScriptBuilder EnableSilentInstall(bool silent)
    {
      script.AllowSilentInstall = silent;
      return this;
    }

    public void Save(string fileName)
    {
      Serializer.Save(script, fileName);
    }
  }
}
