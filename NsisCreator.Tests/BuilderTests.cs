using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NsisCreator.Builder.Tests
{
  [TestClass]
  public class BuilderTests
  {
    [TestMethod]
    public void TestScriptBuilder()
    {
      var builder = new ScriptBuilder();
      var script = builder.SetProductName("Project")
                          .SetProductPublisher("Company")
                          .SetProductVersion("V1.0")
                          .SetOutFileName(@"C:\Temp\Setup.nsi")
                          .ShowDetails(true)
                          .EnableSilentInstall(true)
                          .GetScript();

      Assert.AreEqual("Project", script.ProductName);
      Assert.AreEqual("Company", script.ProductPublisher);
      Assert.AreEqual("V1.0", script.ProductVersion);
      Assert.AreEqual(@"C:\Temp\Setup.nsi", script.OutFileName);
      Assert.IsTrue(script.ShowDetails);
      Assert.IsTrue(script.AllowSilentInstall);
    }

    [TestMethod]
    public void TestMainSectionBuilder()
    {
      var builder = new ScriptBuilder();
      var mainSection = builder.MainSection.SetName("MainSection")
                                           .SetExecutableName("Startup.exe")
                                           .SetOutDir(OutDir.ProgramFiles("MyCompany", "MyProject"))
                                           .Files.AddFromDirectory(@"C:\MyProject\Binaries")
                                                 .AddIncludeFilter(FilterType.EndsWith, ".dll")
                                                 .AddIncludeFilter(FilterType.EndsWith, ".exe").Parent
                                                 .Parent
                                           .AddEnvironmentVariable("Variable1", "Value1")
                                           .Parent
                               .GetScript()
                               .MainSection;

      Assert.AreEqual("MainSection", mainSection.Name);
      Assert.AreEqual("Startup.exe", mainSection.ExecutableName);
      Assert.AreEqual(@"$PROGRAMFILES\MyCompany\MyProject", mainSection.OutDir);
      
      Assert.AreEqual(1, mainSection.Directories.Count);
      Assert.AreEqual(@"C:\MyProject\Binaries", mainSection.Directories[0].DirectoryName);
      
      Assert.AreEqual(2, mainSection.Directories[0].IncludeFilters.Count);
      Assert.AreEqual(FilterType.EndsWith, mainSection.Directories[0].IncludeFilters[0].Type);
      Assert.AreEqual(".dll", mainSection.Directories[0].IncludeFilters[0].Value);
      Assert.AreEqual(FilterType.EndsWith, mainSection.Directories[0].IncludeFilters[1].Type);
      Assert.AreEqual(".exe", mainSection.Directories[0].IncludeFilters[1].Value);
      
      Assert.AreEqual(1, mainSection.EnvironmentVariables.Count);
      Assert.AreEqual("Variable1", mainSection.EnvironmentVariables[0].Name);
      Assert.AreEqual("Value1", mainSection.EnvironmentVariables[0].Value);
    }

    [TestMethod]
    public void TestAdditionalSectionBuilder()
    {
      var builder = new ScriptBuilder();
      var additionalSections = builder.AddAdditionalSection("OtherSection")
                                        .SetOutDir(OutDir.ProgramFiles("MyCompany", "MyProject", "Other"))
                                        .Files.AddFromDirectory(@"C:\MyProject\OtherStuff")
                                          .AddExcludeFilter(FilterType.EndsWith, ".txt").Parent
                                          .Parent
                                        .Parent
                                      .GetScript()
                                      .AdditonalSections;

      Assert.AreEqual(1, additionalSections.Count);
      Assert.AreEqual("OtherSection", additionalSections[0].Name);
      Assert.AreEqual(@"$PROGRAMFILES\MyCompany\MyProject\Other", additionalSections[0].OutDir);

      Assert.AreEqual(1, additionalSections[0].Directories.Count);
      Assert.AreEqual(@"C:\MyProject\OtherStuff", additionalSections[0].Directories[0].DirectoryName);

      Assert.AreEqual(1, additionalSections[0].Directories[0].ExcludeFilters.Count);
      Assert.AreEqual(FilterType.EndsWith, additionalSections[0].Directories[0].ExcludeFilters[0].Type);
      Assert.AreEqual(".txt", additionalSections[0].Directories[0].ExcludeFilters[0].Value);
    }
  }
}
