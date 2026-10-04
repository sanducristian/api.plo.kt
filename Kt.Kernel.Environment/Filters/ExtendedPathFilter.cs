using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Kt.Kernel.Environment.Filters;



/// <summary>
/// ExtendedPathFilter filters based on name, file size, and the last write time of the file.
/// </summary>
/// <remarks>Provides an example of how to customise filtering.</remarks>
public class ExtendedPathFilter : PathFilter {

    long mvarMinSize;

    long mvarMaxSize = long.MaxValue;

    DateTime mvarMinDate = DateTime.MinValue;

    DateTime mvarMaxDate = DateTime.MaxValue;



    /// <summary>
    /// Initialise a new instance of ExtendedPathFilter.
    /// </summary>
    /// <param name="filter">The filter to apply.</param>
    /// <param name="minSize">The minimum file size to include.</param>
    /// <param name="maxSize">The maximum file size to include.</param>
    public ExtendedPathFilter(string filter,
        long minSize, long maxSize)
        : base(filter) {
        MinSize = minSize;
        MaxSize = maxSize;
    }

    /// <summary>
    /// Initialise a new instance of ExtendedPathFilter.
    /// </summary>
    /// <param name="filter">The filter to apply.</param>
    /// <param name="minDate">The minimum <see cref="DateTime"/> to include.</param>
    /// <param name="maxDate">The maximum <see cref="DateTime"/> to include.</param>
    public ExtendedPathFilter(string filter,
        DateTime minDate, DateTime maxDate)
        : base(filter) {
        MinDate = minDate;
        MaxDate = maxDate;
    }

    /// <summary>
    /// Initialise a new instance of ExtendedPathFilter.
    /// </summary>
    /// <param name="filter">The filter to apply.</param>
    /// <param name="minSize">The minimum file size to include.</param>
    /// <param name="maxSize">The maximum file size to include.</param>
    /// <param name="minDate">The minimum <see cref="DateTime"/> to include.</param>
    /// <param name="maxDate">The maximum <see cref="DateTime"/> to include.</param>
    public ExtendedPathFilter(string filter,
        long minSize, long maxSize,
        DateTime minDate, DateTime maxDate)
        : base(filter) {
        MinSize = minSize;
        MaxSize = maxSize;
        MinDate = minDate;
        MaxDate = maxDate;
    }



    /// <summary>
    /// Test a filename to see if it matches the filter.
    /// </summary>
    /// <param name="name">The filename to test.</param>
    /// <returns>True if the filter matches, false otherwise.</returns>
    /// <exception cref="System.IO.FileNotFoundException">The <see paramref="fileName"/> doesnt exist</exception>
    public override bool IsMatch(string name) {
        bool result = base.IsMatch(name);

        if (result) {
            FileInfo fileInfo = new FileInfo(name);
            result =
                (MinSize <= fileInfo.Length) &&
                (MaxSize >= fileInfo.Length) &&
                (MinDate <= fileInfo.LastWriteTime) &&
                (MaxDate >= fileInfo.LastWriteTime)
                ;
        }
        return result;
    }



    /// <summary>
    /// Get/set the minimum size/length for a file that will match this filter.
    /// </summary>
    /// <remarks>The default value is zero.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">value is less than zero; greater than <see cref="MaxSize"/></exception>
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
    /// Get/set the maximum size/length for a file that will match this filter.
    /// </summary>
    /// <remarks>The default value is <see cref="System.Int64.MaxValue"/></remarks>
    /// <exception cref="ArgumentOutOfRangeException">value is less than zero or less than <see cref="MinSize"/></exception>
    public long MaxSize {
        get { return mvarMaxSize; }
        set {
            if ((value < 0) || (mvarMinSize > value)) {
                throw new ArgumentOutOfRangeException("value");
            }

            mvarMaxSize = value;
        }
    }

    /// <summary>
    /// Get/set the minimum <see cref="DateTime"/> value that will match for this filter.
    /// </summary>
    /// <remarks>Files with a LastWrite time less than this value are excluded by the filter.</remarks>
    public DateTime MinDate {
        get { return mvarMinDate; }
        set {
            if (value > mvarMaxDate) {
                throw new ArgumentOutOfRangeException("value", "Exceeds MaxDate");
            }
            mvarMinDate = value;
        }
    }

    /// <summary>
    /// Get/set the maximum <see cref="DateTime"/> value that will match for this filter.
    /// </summary>
    /// <remarks>Files with a LastWrite time greater than this value are excluded by the filter.</remarks>
    public DateTime MaxDate {
        get { return mvarMaxDate; }
        set {
            if (mvarMinDate > value) {
                throw new ArgumentOutOfRangeException("value", "Exceeds MinDate");
            }
            mvarMaxDate = value;
        }
    }
}
