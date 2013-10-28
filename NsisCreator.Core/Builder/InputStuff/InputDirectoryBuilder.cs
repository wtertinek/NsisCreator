using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator.Builder
{
  public class InputDirectoryBuilder<TParent>
  {
    private InputDirectory directory;

    internal InputDirectoryBuilder(InputDirectory directory, TParent parent)
    {
      this.directory = directory;
      Parent = parent;
    }

    public TParent Parent { get; private set; }

    public InputDirectoryBuilder<TParent> AddIncludeFilter(FilterType type, string value)
    {
      return AddFilter(directory.IncludeFilters, type, value, true);
    }

    public InputDirectoryBuilder<TParent> AddIncludeFilter(FilterType type, string value, bool ignoreCase)
    {
      return AddFilter(directory.IncludeFilters, type, value, ignoreCase);
    }

    public InputDirectoryBuilder<TParent> AddExcludeFilter(FilterType type, string value)
    {
      return AddFilter(directory.ExcludeFilters, type, value, true);
    }

    public InputDirectoryBuilder<TParent> AddExcludeFilter(FilterType type, string value, bool ignoreCase)
    {
      return AddFilter(directory.ExcludeFilters, type, value, ignoreCase);
    }

    private InputDirectoryBuilder<TParent> AddFilter(List<Filter> list, FilterType type, string value, bool ignoreCase)
    {
      list.Add(new Filter()
               {
                 Type = type,
                 Value = value,
                 IgnoreCase = ignoreCase
               });
      return this;
    }

    public InputDirectoryBuilder<TParent> SetOverwriteMode(OverwriteMode mode)
    {
      directory.Overwrite = mode;
      return this;
    }
  }
}
