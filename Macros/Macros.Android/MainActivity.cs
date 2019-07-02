using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using Xamarin.Forms;
using Android.Gms.Ads;

//using FootPrint.Droid;

namespace Macros.Droid
{
    [Activity(Label = "FootPrint", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            //global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");


            string fileName = "footprint_db.sqlite";
            string fileLocation = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string fullPath = Path.Combine(fileLocation, fileName);

            MobileAds.Initialize(ApplicationContext, "ca-app-pub-6851704576749823~5571330306");

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(fullPath));
        }
    }
}

