using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NsisCreator.Builder;

namespace NsisCreator.Builder.Tests
{
  public static class ScriptBuilderExtensions
  {
    public static Script GetScript(this ScriptBuilder builder)
    {
      var tmpFile = System.IO.Path.GetTempFileName();
      builder.Save(tmpFile);
      var script = Serializer.Load<Script>(tmpFile);
      System.IO.File.Delete(tmpFile);
      return script;
    }
  }
}
