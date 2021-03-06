﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator
{
  public class Creator
  {
    private Script setup;
    private List<Section> sections;

    public Creator()
    {
      sections = new List<Section>(); 
    }

    public void LoadMainSetup(string fileName)
    {
      setup = Serializer.Load<Script>(fileName);
    }

    public void AddSections(IEnumerable<string> sections)
    {
      foreach (var section in sections)
      {
        AddSection(section);
      }
    }

    public void AddSection(string fileName)
    {
      sections.Add(Serializer.Load<Section>(fileName));
    }

    private void InitSections()
    {
      var tmpSections = new List<Section>(setup.Sections);
      tmpSections.AddRange(sections);
      setup.Sections = tmpSections;
    }

    public void PrintToConsole()
    {
      InitSections();
      var script = setup.Generate();
      Console.Write(script);
    }

    public void SaveToFile(string fileName)
    {
      InitSections();
      var script = setup.Generate();
      Console.Write(script);
      System.IO.File.WriteAllText(fileName, script, Encoding.Default);
    }
  }
}
