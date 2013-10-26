using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator
{
  public static class StringBuilderExtensions
  {
    public static void AppendFormat(this StringBuilder builder, int indent, string value, params object[] args)
    {
      builder.AppendFormat(new string(' ', indent) + value, args);
    }

    public static void AppendLine(this StringBuilder builder, int indent, string value)
    {
      builder.AppendLine(new string(' ', indent) + value);
    }

    public static void AppendLine(this StringBuilder builder, int indent, string formatString, params object[] args)
    {
      builder.AppendFormat(new string(' ', indent) + formatString + "\n", args);
    }

    public static void AppendLine(this StringBuilder builder, string formatString, params object[] args)
    {
      builder.AppendFormat(formatString + "\n", args);
    }
  }
}
