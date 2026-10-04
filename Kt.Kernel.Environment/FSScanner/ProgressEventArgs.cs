using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kt.Kernel.Environment.FSScanner;



/// <summary>
/// Event arguments during processing of a single file or directory.
/// </summary>
public class ProgressEventArgs : EventArgs {
    string mvarName;
    long mvarProcessed;
    long mvarTarget;
    bool continueRunning_ = true;


    /// <summary>
    /// Initialise a new instance of <see cref="ScanEventArgs"/>
    /// </summary>
    /// <param name="name">The file or directory name if known.</param>
    /// <param name="processed">The number of bytes processed so far</param>
    /// <param name="target">The total number of bytes to process, 0 if not known</param>
    public ProgressEventArgs(string name, long processed, long target) {
        mvarName = name;
        mvarProcessed = processed;
        mvarTarget = target;
    }



    /// <summary>
    /// The name for this event if known.
    /// </summary>
    public string Name {
        get { return mvarName; }
    }



    /// <summary>
    /// Get set a value indicating wether scanning should continue or not.
    /// </summary>
    public bool ContinueRunning {
        get { return continueRunning_; }
        set { continueRunning_ = value; }
    }



    /// <summary>
    /// Get a percentage representing how much of the <see cref="Target"></see> has been processed
    /// </summary>
    /// <value>0.0 to 100.0 percent; 0 if target is not known.</value>
    public float PercentComplete {
        get {
            float result;
            if (mvarTarget <= 0) {
                result = 0;
            } else {
                result = ((float)mvarProcessed / (float)mvarTarget) * 100.0f;
            }
            return result;
        }
    }



    /// <summary>
    /// The number of bytes processed so far
    /// </summary>
    public long Processed {
        get { return mvarProcessed; }
    }



    /// <summary>
    /// The number of bytes to process.
    /// </summary>
    /// <remarks>Target may be 0 or negative if the value isnt known.</remarks>
    public long Target {
        get { return mvarTarget; }
    }
}

