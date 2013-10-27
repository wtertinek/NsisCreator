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
    private List<AdditionalSectionBuilder> additionalSections;

    public ScriptBuilder()
    {
      script = new Script();
      MainSection = new MainSectionBuilder(script.MainSection, this);
      additionalSections = new List<AdditionalSectionBuilder>();
    }

    public MainSectionBuilder MainSection { get; private set; }

    public AdditionalSectionBuilder AddAdditionalSection(string sectionName)
    {
      var section = new FileBasedSection() { Name = sectionName };
      script.AdditonalSections.Add(section);
      additionalSections.Add(new AdditionalSectionBuilder(section, this));
      return additionalSections.Last();
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

    public ScriptBuilder SetOutFileName(string outFileName)
    {
      script.OutFileName = outFileName;
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
