using Macros.MenuItems;
using Macros.VersionControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Macros
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class About : ContentPage
	{
        public List<AboutNavigation> AboutMenuList;

        public About ()
		{
			InitializeComponent ();

            var Names = new List<string>
            {
                "Dream","Travel","Time"
            };

            //HomeCarouselView.ItemsSource = Names;


            var images = new List<string>
            {
                "https://78.media.tumblr.com/9e8ccd15f0a83dcb1e9f54d5849ed6a5/tumblr_p8ncl8WrPL1v2d0ejo1_1280.jpg",
                "https://78.media.tumblr.com/88302e4c6979cfcdaaa4e732564ffe18/tumblr_p8jfn5mfca1qevwuho1_500.jpg",
                "https://78.media.tumblr.com/c0f2739822e343c61c4ed8eb1bf9f631/tumblr_p8q9fhvnbd1xppqf9o1_1280.jpg"
            };

            //HomeCarouselView.ItemsSource = images;


            //Content Navigation part
            //version control
            var version = DependencyService.Get<IAppVersionProvider>();
            var versionString = version.AppVersion;
            //versionLabel.Text = versionString + " | devXstudio";
            //end of version control

            AboutMenuList = new List<AboutNavigation>();
            //this are for android Icons you can download from android asset studio and include in Your Project Resources Folder
            // Creating our pages for menu navigation
            // Here you can define title for item, 
            // icon on the left side, and page that you want to open after selection
            var page1 = new AboutNavigation() { Title = "Dashboard", Icon = "round_dashboard_black_24.xml", Details = versionString, TargetType = typeof(HomePage) };
            var page2 = new AboutNavigation() { Title = "Calculate", Icon = "round_wc_black_24.xml", Details = "", TargetType = typeof(Calculate) };
            //var page3 = new AboutNavigation() { Title = "History", Icon = "round_assignment_ind_black_24.xml", Details = "",TargetType = typeof(DisplayData) };
            //var page4 = new AboutNavigation() { Title = "Settings", Icon = "round_settings_black_24.xml", Details = "", TargetType = typeof(Settings) };
            var page5 = new AboutNavigation() { Title = "About", Icon = "round_help_black_24.xml", Details = versionString, TargetType = typeof(About) };


            ////Fot Ios icons
            //var page1 = new MasterPageItem() { Title = "FastDelivery", Icon = "ic_local_shipping.png", TargetType = typeof(View1) };
            //var page2 = new MasterPageItem() { Title = "Menus", Icon = "ic_restaurant.png", TargetType = typeof(View1) };
            //var page3 = new MasterPageItem() { Title = "Free Pizza", Icon = "ic_local_pizza.png", TargetType = typeof(View1) };
            //var page4 = new MasterPageItem() { Title = "Dining", Icon = "ic_local_dining.png", TargetType = typeof(View1) };
            //var page5 = new MasterPageItem() { Title = "Parking", Icon = "ic_local_parking.png", TargetType = typeof(View1) };
            //var page6 = new MasterPageItem() { Title = "LocateUs", Icon = "ic_my_location.png", TargetType = typeof(View1) };

            // Adding menu items to menuList
            //AboutMenuList.Add(page1);
            //AboutMenuList.Add(page2);
            //AboutMenuList.Add(page3);
            //AboutMenuList.Add(page4);
            AboutMenuList.Add(page5);
            //menuList.Add(page6);


            // Setting our list to be ItemSource for ListView in MainPage.xaml
            AboutNavigationList.ItemsSource = AboutMenuList;
        }

        //private void AboutNavigationList_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        //{

        //}
    }
}