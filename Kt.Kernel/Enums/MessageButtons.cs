using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kt.Kernel;

public enum QbMessageButtons {
    AbortRetryIgnore = 64,
    OK = 128,
    OKCancel = 256,
    RetryCancel = 512,
    YesNo = 1024,
    YesNoCancel = 2048
}
