# NsisCreator

NsisCreator is a simple library written in C# that lets you generate NSIS scripts from definitions stored in XML files. You can use NsisCreator in your build process to generate NSIS scripts on-the-fly at build time, to minimize the effort of manually maintaining your scripts. Furthermore NsisCreator enables you to modularize your NSIS scripts, if you have to create several scripts that are similar, but differ in some aspects.

## Example

The following code creates a XML file that captures the *static* definition of your NSIS project. **This step has to be done only once.** Besides general informations like product name, product version and company name, the XML file contains the location and file extensions of all relevant files, but not their exact names. In this way you do not have to make any changes if new files have to be added to the setup project. 

Here's the code to generate the XML file: 

```csharp
var builder = new ScriptBuilder();
builder.SetProductName("Backup3000")
       .SetProductPublisher("Up2-11")
       .SetProductVersion("1.0")
       .SetOutFileName(Variable.Create("OUT_FILE"))
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
       .Save(@"C:\Build\Backup3000.xml");
```

We use the variable *OUT_FILE* so we can provide the name of the final EXE file at runtime.

With the XML file at hand you can integrate NsisCreator into your build process. You simply have to call NsisCreator and provide the XML file and define the name of the output file: 

	NsisCreator -i C:\Build\Backup3000.xml -o C:\Build\Backup3000.nsi

This creates the file *C:\Build\Backup3000.nsi* which now can be compiled:

	makensis /DOUT_FILE=C:\Deployment\Backup3000_Setup.exe C:\Build\Backup3000.nsi

This step finally creates our setup package *C:\Deployment\Backup3000_Setup.exe*

## Modules

Based on the previous example, we now can build two different variants of our application, one for a test environment and one for a production environment. We assume that the two variants have different configuration files and that the production variant needs some additional PDF files which are placed in AppData.

#### Test Environment

Let's start with the variant for the test environment. We create a configuration that adds a configuration file to the project (remember, this has to be done only once):

```csharp
var builder = new ScriptBuilder();
builder.AddAdditionalSection("Test Configuration")
         .SetOutDir(OutDir.ProgramFiles("Up2-11", "Backup3000"))
         .Files.AddFile(@"C:\Development\Configuration\Test.config")
                 .SetTargetName("Startup.config").Parent.Parent
         .Save(@"C:\Build\TestConfiguration.xml");
```

In the build process we now can create the test version based on the previously created main definition:

	NsisCreator -i C:\Build\Backup3000.xml -a C:\Build\TestConfiguration.xml -o C:\Build\Backup3000.nsi 

	makensis /DOUT_FILE=C:\Deployment\Test\Backup3000_Setup.exe C:\Build\Backup3000.nsi

#### Prod Environment

We again create a XML file for the configuration file (or we could simply copy and adjust the file TestConfiguration.xml):

```csharp
var builder = new ScriptBuilder();
builder.AddAdditionalSection("Prod Configuration")
         .SetOutDir(OutDir.ProgramFiles("Up2-11", "Backup3000"))
         .Files.AddFile(@"C:\Development\Configuration\Prod.config")
                 .SetTargetName("Startup.config").Parent.Parent
         .Save(@"C:\Build\ProdConfiguration.xml");
```

And now the definition for the PDF files we need for production: 

```csharp
var builder = new ScriptBuilder();
builder.AddAdditionalSection("PDF files")
         .SetOutDir(OutDir.AppData("Up2-11", "Backup3000"))
         .Files.AddFromDirectory(@"C:\Development\Data").Parent.Parent
         .Save(@"C:\Build\Data.xml");
```

Looking at the build process again, we now can create the final production version based on the previously created main definition:

	NsisCreator -i C:\Build\Backup3000.xml -a C:\Build\ProdConfiguration.xml -a C:\Build\Data.xml -o C:\Build\Backup3000.nsi 

	makensis /DOUT_FILE=C:\Deployment\Test\Backup3000_Setup.exe C:\Build\Backup3000.nsi