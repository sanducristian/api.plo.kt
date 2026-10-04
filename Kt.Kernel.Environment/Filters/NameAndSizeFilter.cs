using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Kt.Kernel.Environment.Filters;


/// <summary>
/// NameAndSizeFilter filters based on name and file size.
/// </summary>
/// <remarks>A sample showing how filters might be extended.</remarks>
[Obsolete("Use ExtendedPathFilter instead")]
public class NameAndSizeFilter : PathFilter {
    #region Instance Fields
    long mvarMinSize;
    long mvarMaxSize = long.MaxValue;
    #endregion

    /// <summary>
    /// Initialise a new instance of NameAndSizeFilter.
    /// </summary>
    /// <param name="filter">The filter to apply.</param>
    /// <param name="minSize">The minimum file size to include.</param>
    /// <param name="maxSize">The maximum file size to include.</param>
    public NameAndSizeFilter(string filter, long minSize, long maxSize)
        : base(filter) {
        MinSize = minSize;
        MaxSize = maxSize;
    }

    /// <summary>
    /// Test a filename to see if it matches the filter.
    /// </summary>
    /// <param name="name">The filename to test.</param>
    /// <returns>True if the filter matches, false otherwise.</returns>
    public override bool IsMatch(string name) {
        bool result = base.IsMatch(name);

        if (result) {
            FileInfo fileInfo = new FileInfo(name);
            long length = fileInfo.Length;
            result =
                (MinSize <= length) &&
                (MaxSize >= length);
        }
        return result;
    }

    /// <summary>
    /// Get/set the minimum size for a file that will match this filter.
    /// </summary>
    public long MinSize {
        get { return mvarMinSize; }
        set {
            if ((value < 0) || (mvarMaxSize < value)) {
                throw new ArgumentOutOfRangeException("value");
            }

            mvarMinSize = value;
        }
    }

    /// <summary>
    /// Get/set the maximum size for a file that will match this filter.
    /// </summary>
    public long MaxSize {
        get { return mvarMaxSize; }
        set {
            if ((value < 0) || (mvarMinSize > value)) {
                throw new ArgumentOutOfRangeException("value");
            }
            mvarMaxSize = value;
        }
    }
}
