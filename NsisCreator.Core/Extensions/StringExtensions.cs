using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator
{
  public static class StringExtensions
  {
    public static bool IsOneOf(this string @string, params string[] values)
    {
      return values.Contains(@string);
    }
    
    public static bool StartsWithOneOf(this string @string, params string[] values)
    {
      return values.Any(v => @string.StartsWith(v));
    }
  }
}
