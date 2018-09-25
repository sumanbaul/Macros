using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Macros.Droid;
using Macros.VersionControl;
using Xamarin.Forms;

[assembly: Dependency(typeof(VersionControl_Android))]
namespace Macros.Droid
{
    public class VersionControl_Android : IAppVersionProvider
    {
        public string AppVersion { get; set; }

        public VersionControl_Android()
        {

            var context = Android.App.Application.Context;
            var info = context.PackageManager.GetPackageInfo(context.PackageName, 0);
            //AppVersion = info.VersionName.ToString();
            AppVersion = $"{ info.VersionCode.ToString()}.{info.VersionName}";
            //return $"{info.VersionName}.{info.VersionCode.ToString()}";

        }
    }
}
