using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator
{
  public class Parameters
  {
    public Parameters(string[] args)
    {
      IsSyntacticallyValid = args.Length % 2 == 0 &&
                args.Where((a, i) => i % 2 == 0)
                    .All(a => a.IsOneOf("-i", "-e", "-o", "-a")) &&
                args.Count(a => a == "-i") == 1 &&
                args.Count(a => a == "-e") <= 1 &&
                args.Count(a => a == "-o") <= 1;

      if (!IsSyntacticallyValid)
      {
        return;
      }

      var additionalSections = new List<string>();

      for (int i = 0; i < args.Length; i += 2)
      {
        switch (args[i])
        {
          case "-i":
            InputFile = args[i + 1];
            break;
          case "-e":
            ExecutionPath = args[i + 1];
            break;
          case "-o":
            OutputFile = args[i + 1];
            break;
          case "-a":
            additionalSections.Add(args[i + 1]);
            break;
        }
      }

      Sections = additionalSections.ToArray();
    }

    public bool IsSyntacticallyValid { get; private set; }

    public string InputFile { get; private set; }

    public string OutputFile { get; private set; }

    public bool HasOutputFile
    {
      get { return !string.IsNullOrEmpty(OutputFile); }
    }

    public string ExecutionPath { get; private set; }

    public bool HasExecutionPath
    {
      get { return !string.IsNullOrEmpty(ExecutionPath); }
    }

    public string[] Sections { get; private set; }
  }
}
