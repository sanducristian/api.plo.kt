using System;
using System.IO;

namespace Kt.Kernel.Environment.Filters;


/// <summary>
/// PathFilter filters directories and files using a form of <see cref="System.Text.RegularExpressions.Regex">regular expressions</see>
/// by full path name.
/// See <see cref="NameFilter">NameFilter</see> for more detail on filtering.
/// </summary>
public class PathFilter : IScanFilter {


    /// <summary>
    /// The <see cref="NameFilter">filter</see> expression to apply.
    /// </summary>
    NameFilter mvarNameFilter;


    /// <summary>
    /// Initialise a new instance of <see cref="PathFilter"></see>.
    /// </summary>
    /// <param name="filter">The <see cref="NameFilter">filter</see> expression to apply.</param>
    public PathFilter(string filter) {
        mvarNameFilter = new NameFilter(filter);
    }



    /// <summary>
    /// Test a name to see if it matches the filter.
    /// </summary>
    /// <param name="name">The name to test.</param>
    /// <returns>True if the name matches, false otherwise.</returns>
    /// <remarks><see cref="Path.GetFullPath(string)"/> is used to get the full path before matching.</remarks>
    public virtual bool IsMatch(string name) {
        bool result = false;

        if (name != null) {
            string cooked = (name.Length > 0) ? Path.GetFullPath(name) : "";
            result = mvarNameFilter.IsMatch(cooked);
        }
        return result;
    }
}

