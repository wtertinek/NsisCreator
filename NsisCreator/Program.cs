using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator
{
  class Program
  {
    static void Main(string[] args)
    {
      var parameters = new Parameters(args);

      if (!parameters.IsSyntacticallyValid)
      {
        Console.WriteLine("Parameters not valid.");
        Console.WriteLine("Usage: -i InputFile [-e ExecutionPath]? [-o OutputFile]? [-a SectionFile]*");
      }
      else
      {
        if (parameters.HasExecutionPath)
        {
          CheckDirectory(parameters.ExecutionPath);
          Environment.CurrentDirectory = parameters.ExecutionPath;
        }

        CheckFile(parameters.InputFile);
        parameters.Sections.ToList().ForEach(s => CheckFile(s));

        var generator = new Creator();
        generator.LoadMainSetup(parameters.InputFile);
        generator.AddSections(parameters.Sections);

        if (parameters.HasOutputFile)
        {
          generator.SaveToFile(parameters.OutputFile);
        }
        else
        {
          generator.PrintToConsole();
        }
      }
    }

    private static void CheckDirectory(string directoryName)
    {
      if (!System.IO.Directory.Exists(directoryName))
      {
        throw new System.IO.DirectoryNotFoundException("Directory \"" + directoryName + "\" not found.");
      }
    }

    private static void CheckFile(string fileName)
    {
      if (!System.IO.File.Exists(fileName))
      {
        throw new System.IO.FileNotFoundException("File \"" + fileName + "\" not found", fileName);
      }
    }
  }
}
