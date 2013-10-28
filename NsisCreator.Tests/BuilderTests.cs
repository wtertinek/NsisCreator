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
                                           .SetOutDir(Directory.ProgramFiles("MyCompany", "MyProject"))
                                           .Files.AddFromDirectory(@"C:\MyProject\Binaries")
                                                 .AddIncludeFilter(FilterType.EndsWith, ".dll")
                                                 .AddIncludeFilter(FilterType.EndsWith, ".exe").Parent
                                                 .Parent
                                           .AddEnvironmentVariable("Variable1", "Value1")
                                           .Create(Directory.StartMenuPrograms("MyCompany", "MyProject"))
                                             .AddShortCut("The Program", "Startup.exe")
                                             .AddShortCutToUninstall("Uninstall")
                                             .Parent
                                           .Parent
                               .GetScript()
                               .MainSection;

      Assert.AreEqual("MainSection", mainSection.Name);
      Assert.AreEqual("Startup.exe", mainSection.ExecutableName);
      Assert.AreEqual(@"$PROGRAMFILES\MyCompany\MyProject", mainSection.OutDir);
      
      Assert.AreEqual(1, mainSection.InputDirectories.Count);
      Assert.AreEqual(@"C:\MyProject\Binaries", mainSection.InputDirectories[0].DirectoryName);
      
      Assert.AreEqual(2, mainSection.InputDirectories[0].IncludeFilters.Count);
      Assert.AreEqual(FilterType.EndsWith, mainSection.InputDirectories[0].IncludeFilters[0].Type);
      Assert.AreEqual(".dll", mainSection.InputDirectories[0].IncludeFilters[0].Value);
      Assert.AreEqual(FilterType.EndsWith, mainSection.InputDirectories[0].IncludeFilters[1].Type);
      Assert.AreEqual(".exe", mainSection.InputDirectories[0].IncludeFilters[1].Value);
      
      Assert.AreEqual(1, mainSection.EnvironmentVariables.Count);
      Assert.AreEqual("Variable1", mainSection.EnvironmentVariables[0].Name);
      Assert.AreEqual("Value1", mainSection.EnvironmentVariables[0].Value);

      Assert.AreEqual(1, mainSection.Directories.Count);
      Assert.AreEqual(@"$SMPROGRAMS\MyCompany\MyProject", mainSection.Directories[0].Path);
      Assert.AreEqual(2, mainSection.Directories[0].ShortCuts.Count);
      Assert.AreEqual(@"$SMPROGRAMS\MyCompany\MyProject\The Program", mainSection.Directories[0].ShortCuts[0].Path);
      Assert.AreEqual("Startup.exe", mainSection.Directories[0].ShortCuts[0].TargetPath);
      Assert.AreEqual(@"$SMPROGRAMS\MyCompany\MyProject\Uninstall", mainSection.Directories[0].ShortCuts[1].Path);
      Assert.AreEqual(@"$INSTDIR\uninst.exe", mainSection.Directories[0].ShortCuts[1].TargetPath);
    }

    [TestMethod]
    public void TestAdditionalSectionBuilder()
    {
      var builder = new ScriptBuilder();
      var additionalSections = builder.AddAdditionalSection("OtherSection")
                                        .SetOutDir(Directory.ProgramFiles("MyCompany", "MyProject", "Other"))
                                        .Files.AddFromDirectory(@"C:\MyProject\OtherStuff")
                                          .AddExcludeFilter(FilterType.EndsWith, ".txt").Parent
                                          .Parent
                                        .Parent
                                      .GetScript()
                                      .AdditonalSections;

      Assert.AreEqual(1, additionalSections.Count);
      Assert.AreEqual("OtherSection", additionalSections[0].Name);
      Assert.AreEqual(@"$PROGRAMFILES\MyCompany\MyProject\Other", additionalSections[0].OutDir);

      Assert.AreEqual(1, additionalSections[0].InputDirectories.Count);
      Assert.AreEqual(@"C:\MyProject\OtherStuff", additionalSections[0].InputDirectories[0].DirectoryName);

      Assert.AreEqual(1, additionalSections[0].InputDirectories[0].ExcludeFilters.Count);
      Assert.AreEqual(FilterType.EndsWith, additionalSections[0].InputDirectories[0].ExcludeFilters[0].Type);
      Assert.AreEqual(".txt", additionalSections[0].InputDirectories[0].ExcludeFilters[0].Value);
    }
  }
}
