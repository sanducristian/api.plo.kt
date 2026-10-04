using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kt.Kernel.Environment.FSScanner;


/// <summary>
/// Event arguments for scanning.
/// </summary>
public class ScanEventArgs : EventArgs {
    string mvarName;

    bool continueRunning_ = true;


    /// <summary>
    /// Initialise a new instance of <see cref="ScanEventArgs"/>
    /// </summary>
    /// <param name="name">The file or directory name.</param>
    public ScanEventArgs(string name) {
        mvarName = name;
    }


    /// <summary>
    /// The file or directory name for this event.
    /// </summary>
    public string Name {
        get { return mvarName; }
    }



    /// <summary>
    /// Get set a value indicating if scanning should continue or not.
    /// </summary>
    public bool ContinueRunning {
        get { return continueRunning_; }
        set { continueRunning_ = value; }
    }
}

