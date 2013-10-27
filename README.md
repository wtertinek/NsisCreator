# NsisCreator

NsisCreator is a simple library written in C# that lets you generate NSIS scripts from definitions stored in XML files. You can use NsisCreator in your build process to generate NSIS scripts on-the-fly at build time, to minimize the effort of manually maintaining your scripts. Furthermore NsisCreator enables to modularize your NSIS scripts, if you have to create several scripts that are similar, but differ in some aspects.

Example:

The following code creates an XML file that captures the *static* definition of your NSIS project. **This step has to be done only once.** Besides general information like product name, product version and company name, the XML file contains the location and file extensions of all relevant files, but not their exact names. In this way you don't have to make any changes to your NSIS scripts if new files have to be added to the setup project. 

Here's the code to generate the XML file: 

```csharp
var builder = new ScriptBuilder();
builder.SetProductName("Backup3000")
       .SetProductPublisher("Up2-11")
       .SetProductVersion("1.0")
       .SetOutFileName(@"C:\Deployment\Backup3000_Setup.exe")
       .MainSection.SetName("MainSection")
                   .SetExecutableName("Startup.exe")
                   .SetOutDir(OutDir.ProgramFiles("Up2-11", "Backup3000"))
                   .Files.AddFromDirectory(@"C:\Development\Backup3000\Binaries")
                           .AddIncludeFilter(FilterType.EndsWith, ".dll")
                           .AddIncludeFilter(FilterType.EndsWith, ".exe").Parent
					     .AddFromDirectory(@"C:\Development\Backup3000\Libraries")
                           .AddIncludeFilter(FilterType.EndsWith, ".dll").Parent
                         .Parent
                   .Parent
       .Save(@"C:\Temp\Backup3000.xml");
```

With the XML file at hand you you can integrate NsisCreator into your build process. You simply have to call NsisCreator and provide the XML file and define the name of the output file: 

	NsisCreator -i C:\Build\Backup3000.xml -o C:\Build\Backup3000.nsi

Now you can compile the setup:

	makensis C:\Build\Backup3000.nsi