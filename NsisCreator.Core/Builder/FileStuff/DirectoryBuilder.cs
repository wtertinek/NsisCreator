using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisCreator.Builder
{
  public class DirectoryBuilder<TParent>
  {
    private Directory directory;

    internal DirectoryBuilder(Directory directory, TParent parent)
    {
      this.directory = directory;
      Parent = parent;
    }

    public TParent Parent { get; private set; }

    public DirectoryBuilder<TParent> AddIncludeFilter(FilterType type, string value)
    {
      return AddFilter(directory.IncludeFilters, type, value, true);
    }

    public DirectoryBuilder<TParent> AddIncludeFilter(FilterType type, string value, bool ignoreCase)
    {
      return AddFilter(directory.IncludeFilters, type, value, ignoreCase);
    }

    public DirectoryBuilder<TParent> AddExcludeFilter(FilterType type, string value)
    {
      return AddFilter(directory.ExcludeFilters, type, value, true);
    }

    public DirectoryBuilder<TParent> AddExcludeFilter(FilterType type, string value, bool ignoreCase)
    {
      return AddFilter(directory.ExcludeFilters, type, value, ignoreCase);
    }

    private DirectoryBuilder<TParent> AddFilter(List<Filter> list, FilterType type, string value, bool ignoreCase)
    {
      list.Add(new Filter()
               {
                 Type = type,
                 Value = value,
                 IgnoreCase = ignoreCase
               });
      return this;
    }

    public DirectoryBuilder<TParent> SetOverwriteMode(OverwriteMode mode)
    {
      directory.Overwrite = mode;
      return this;
    }
  }
}
