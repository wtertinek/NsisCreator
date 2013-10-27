# NsisCreator

NsisCreator is a simple library written in C# that lets you generate NSIS scripts from definitions stored in XML files. You can use NsisCreator in your build process to generate NSIS scripts on-the-fly at build time, to minimize the effort of manually maintaining your scripts.

```csharp
      var builder = new ScriptBuilder();
      builder.SetProductName("Project")
             .SetProductPublisher("Company")
             .SetProductVersion("1.0")
             .SetOutFileName(@"C:\Setup\Setup.nsi")
             .MainSection.SetName("MainSection")
                         .SetExecutableName("Startup.exe")
                         .SetOutDir(OutDir.ProgramFiles("Company", "Project"))
                         .Files.AddFromDirectory(@"C:\Project\Binaries")
                               .AddIncludeFilter(FilterType.EndsWith, ".dll")
                               .AddIncludeFilter(FilterType.EndsWith, ".exe").Parent
                               .Parent
                         .Parent
             .Save(@"C:\Temp\MainSection.xml");
```