using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NsisCreator.Tests
{
  [TestClass]
  public class ParametersTest
  {
    // We assume that these files exist
    private static string ExistingFile1 = @"C:\Windows\notepad.exe";
    private static string ExistingFile2 = @"C:\Windows\regedit.exe";
    private static string ExistingFile3 = @"C:\Windows\hh.exe";

    // We assume that this path exists
    private static string Path = @"C:\Windows";
    
    [TestMethod]
    public void TestValidArgs()
    {
      var parameters = new Parameters(new [] { "-i", ExistingFile1, "-e", Path, "-o", ExistingFile2, "-a", ExistingFile3 });
      Assert.IsTrue(parameters.IsSyntacticallyValid);
      Assert.AreEqual(ExistingFile1, parameters.InputFile);
      Assert.AreEqual(ExistingFile2, parameters.OutputFile);
      Assert.AreEqual(ExistingFile3, parameters.Sections.First());
      Assert.AreEqual(Path, parameters.ExecutionPath);

      parameters = new Parameters(new [] { "-e", Path, "-a", ExistingFile3, "-i", ExistingFile1, "-o", ExistingFile2 });
      Assert.IsTrue(parameters.IsSyntacticallyValid);
      Assert.AreEqual(ExistingFile1, parameters.InputFile);
      Assert.AreEqual(ExistingFile2, parameters.OutputFile);
      Assert.AreEqual(ExistingFile3, parameters.Sections.First());
      Assert.AreEqual(Path, parameters.ExecutionPath);
    }

    [TestMethod]
    public void TestInvalidArgs()
    {
      var parameters = new Parameters(new [] { ExistingFile1, "-o", ExistingFile2 });
      Assert.IsFalse(parameters.IsSyntacticallyValid);

      parameters = new Parameters(new [] { "-i", ExistingFile1, ExistingFile2 });
      Assert.IsFalse(parameters.IsSyntacticallyValid);

      parameters = new Parameters(new [] { "-o", ExistingFile2 });
      Assert.IsFalse(parameters.IsSyntacticallyValid);

      parameters = new Parameters(new [] { "-i", ExistingFile1, "-i", ExistingFile1 });
      Assert.IsFalse(parameters.IsSyntacticallyValid);

      parameters = new Parameters(new [] { "-i", ExistingFile1, "-o", ExistingFile2, "-o", ExistingFile2 });
      Assert.IsFalse(parameters.IsSyntacticallyValid);
    }
  }
}
