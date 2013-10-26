using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator.Builder
{
  public class AdditionalDirectoryBuilder
  {
    private Directory directory;

    internal AdditionalDirectoryBuilder(string directoryName, AdditionalSectionBuilder parent)
    {
      directory = new Directory();
      directory.DirectoryName = directoryName;
      Parent = parent;
    }

    public AdditionalSectionBuilder Parent { get; private set; }

    public AdditionalDirectoryBuilder AddIncludeFilter(FilterType type, string value)
    {
      return AddFilter(directory.IncludeFilters, type, value, true);
    }

    public AdditionalDirectoryBuilder AddIncludeFilter(FilterType type, string value, bool ignoreCase)
    {
      return AddFilter(directory.IncludeFilters, type, value, ignoreCase);
    }

    public AdditionalDirectoryBuilder AddExcludeFilter(FilterType type, string value)
    {
      return AddFilter(directory.ExcludeFilters, type, value, true);
    }

    public AdditionalDirectoryBuilder AddExcludeFilter(FilterType type, string value, bool ignoreCase)
    {
      return AddFilter(directory.ExcludeFilters, type, value, ignoreCase);
    }

    private AdditionalDirectoryBuilder AddFilter(List<Filter> list, FilterType type, string value, bool ignoreCase)
    {
      list.Add(new Filter()
               {
                 Type = type,
                 Value = value,
                 IgnoreCase = ignoreCase
               });
      return this;
    }

    public AdditionalDirectoryBuilder SetOverwriteMode(OverwriteMode mode)
    {
      directory.Overwrite = mode;
      return this;
    }
  }
}
