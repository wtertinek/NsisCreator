using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator.Builder
{
  public class MainDirectoryBuilder
  {
    private Directory directory;

    internal MainDirectoryBuilder(string directoryName, MainSectionBuilder parent)
    {
      directory = new Directory();
      directory.DirectoryName = directoryName;
      Parent = parent;
    }

    public MainSectionBuilder Parent { get; private set; }

    public MainDirectoryBuilder AddIncludeFilter(FilterType type, string value)
    {
      return AddFilter(directory.IncludeFilters, type, value, true);
    }

    public MainDirectoryBuilder AddIncludeFilter(FilterType type, string value, bool ignoreCase)
    {
      return AddFilter(directory.IncludeFilters, type, value, ignoreCase);
    }

    public MainDirectoryBuilder AddExcludeFilter(FilterType type, string value)
    {
      return AddFilter(directory.ExcludeFilters, type, value, true);
    }

    public MainDirectoryBuilder AddExcludeFilter(FilterType type, string value, bool ignoreCase)
    {
      return AddFilter(directory.ExcludeFilters, type, value, ignoreCase);
    }

    private MainDirectoryBuilder AddFilter(List<Filter> list, FilterType type, string value, bool ignoreCase)
    {
      list.Add(new Filter()
               {
                 Type = type,
                 Value = value,
                 IgnoreCase = ignoreCase
               });
      return this;
    }

    public MainDirectoryBuilder SetOverwriteMode(OverwriteMode mode)
    {
      directory.Overwrite = mode;
      return this;
    }
  }
}
