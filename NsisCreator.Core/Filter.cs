using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator
{
  [DebuggerDisplay("{Type} {Value}")]
  public class Filter
  {
    public Filter()
    {
      IgnoreCase = true;
    }

    public string Value { get; set; }

    public bool IgnoreCase { get; set; }

    public FilterType Type { get; set; }

    public bool Match(string value)
    {
      var comp = IgnoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture;

      if (Type == FilterType.Contains)
      {
        if (IgnoreCase)
        {
          return value.ToLower().Contains(Value.ToLower());
        }
        else
        {
          return value.Contains(Value);
        }
      }
      else if (Type == FilterType.StartsWith)
      {
        return value.StartsWith(Value, comp);
      }
      else
      {
        return value.EndsWith(Value, comp);
      }
    }

    public static Filter Executables
    {
      get
      {
        return new Filter()
               {
                 Value = "exe",
                 Type = FilterType.EndsWith
               };
      }
    }

    public static Filter Libraries
    {
      get
      {
        return new Filter()
               {
                 Value = "dll",
                 Type = FilterType.EndsWith
               };
      }
    }
  }
}
