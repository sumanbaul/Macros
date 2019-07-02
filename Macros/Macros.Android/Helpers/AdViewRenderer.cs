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
using Android.Gms.Ads;
using Xamarin.Forms;
using Macros.Droid.Helpers;
using Macros;
using Xamarin.Forms.Platform.Android;
using Macros.Controls;

[assembly: ExportRenderer(typeof(Macros.Controls.AdControlView), typeof(AdViewRenderer))]
namespace Macros.Droid.Helpers
{

#pragma warning disable CS0618 // Type or member is obsolete
    public class AdViewRenderer : ViewRenderer<Controls.AdControlView, AdView>
    {
            string adUnitId = "ca-app-pub-6851704576749823/9845439624";
        AdSize adSize = AdSize.SmartBanner;
        AdView adView;

        AdView CreateAdView()
        {
            if (adView != null)
                return adView;
            adView = new AdView(Forms.Context);
            adView.AdSize = AdSize.Banner;
            adView.AdUnitId = adUnitId;
            var adParams = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
            adView.LayoutParameters = adParams;

            adView.LoadAd(new AdRequest.Builder().Build());

            return adView;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Controls.AdControlView> e)
        {
            base.OnElementChanged(e);

            if(Control == null)
            {
                CreateAdView();
                SetNativeControl(adView);
            }
        }
    }
//#pragma warning restore CS0618 // Type or member is obsolete

    // AppID: ca-app-pub-6851704576749823~5571330306
}