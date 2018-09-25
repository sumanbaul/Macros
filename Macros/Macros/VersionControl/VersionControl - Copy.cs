using System;
using System.Collections.Generic;
using System.Text;

namespace Macros.VersionControl
{
    public interface IAppVersionProvider
    {
        string AppVersion { get; }
    }
}
