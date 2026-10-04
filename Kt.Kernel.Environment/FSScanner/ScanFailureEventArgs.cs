using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kt.Kernel.Environment.FSScanner;


/// <summary>
/// Arguments passed when scan failures are detected.
/// </summary>
public class ScanFailureEventArgs : EventArgs {


    string mvarName;


    Exception mobjException;


    bool mvarContinueRunning = true;



    /// <summary>
    /// Initialise a new instance of <see cref="ScanFailureEventArgs"></see>
    /// </summary>
    /// <param name="name">The name to apply.</param>
    /// <param name="e">The exception to use.</param>
    public ScanFailureEventArgs(string name, Exception e) {
        mvarName = name;
        mobjException = e;
        mvarContinueRunning = true;
    }



    /// <summary>
    /// The applicable name.
    /// </summary>
    public string Name {
        get { return mvarName; }
    }



    /// <summary>
    /// The applicable exception.
    /// </summary>
    public Exception Exception {
        get { return mobjException; }
    }



    /// <summary>
    /// Get / set a value indicating wether scanning should continue.
    /// </summary>
    public bool ContinueRunning {
        get { return mvarContinueRunning; }
        set { mvarContinueRunning = value; }
    }
}

