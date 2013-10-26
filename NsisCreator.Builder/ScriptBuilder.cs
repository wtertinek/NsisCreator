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
    private MainSectionBuilder mainSection;
    private List<AdditionalSectionBuilder> additionalSections;

    public ScriptBuilder()
    {
      script = new Script();
      mainSection = new MainSectionBuilder(this);
      additionalSections = new List<AdditionalSectionBuilder>();
    }

    public MainSectionBuilder MainSection { get; private set; }

    public AdditionalSectionBuilder AddAdditionalSection(string sectionName)
    {
      additionalSections.Add(new AdditionalSectionBuilder(sectionName, this));
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

    public ScriptBuilder SilentInstall(bool silent)
    {
      script.Silent = silent;
      return this;
    }

    public void Save(string fileName)
    {
      Serializer.Save(script, fileName);
    }
  }
}
