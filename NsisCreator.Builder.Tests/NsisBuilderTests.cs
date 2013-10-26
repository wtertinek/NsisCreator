using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NsisCreator.Builder.Tests
{
  [TestClass]
  public class NsisBuilderTests
  {
    [TestMethod]
    public void TestMethod1()
    {
      var builder = new ScriptBuilder();
      builder.SetProductName("MyProject")
             .SetProductPublisher("MyCompany")
             .SetProductVersion("V1.0")
             .SetOutFileName(@"C:\Temp\Setup.nsi")
             .MainSection.AddDirectory(@"C:\MyProject\Binaries")
                         .AddIncludeFilter(FilterType.EndsWith, ".dll")
                         .AddIncludeFilter(FilterType.EndsWith, ".exe");
             //            .Parent
             //.SetExecutableName("Startup.exe")
             //.AddShortCut(
    }
  }
}
