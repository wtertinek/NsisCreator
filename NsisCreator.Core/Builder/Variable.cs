using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator.Builder
{
  public static class Variable
  {
    public static string Create(string name)
    {
      return "${" + name + "}";
    }
  }
}
