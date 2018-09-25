using Macros.MenuItems;
using Macros.VersionControl;
//using Macros.VersionControl;
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
    public partial class MenuPage : MasterDetailPage
    {
        public List<MenuPageItem> menuList { get; set; }

        public MenuPage()
        {
            InitializeComponent();

            //Detail = new NavigationPage(new HomePage());

            //IsPresented = false;

            //version control
            var version = DependencyService.Get<IAppVersionProvider>();
            var versionString = version.AppVersion;
            versionLabel.Text = versionString + " | devXstudio";
            //end of version control

            menuList = new List<MenuPageItem>();
            //this are for android Icons you can download from android asset studio and include in Your Project Resources Folder
            // Creating our pages for menu navigation
            // Here you can define title for item, 
            // icon on the left side, and page that you want to open after selection
            var page1 = new MenuPageItem() { Title = "Dashboard", Icon = "round_dashboard_black_24.xml", TargetType = typeof(HomePage) };
            var page2 = new MenuPageItem() { Title = "Calculate", Icon = "round_wc_black_24.xml", TargetType = typeof(Calculate) };
            //var page3 = new MenuPageItem() { Title = "History", Icon = "round_assignment_ind_black_24.xml", TargetType = typeof(DisplayData) };
            //var page4 = new MenuPageItem() { Title = "Settings", Icon = "round_settings_black_24.xml", TargetType = typeof(Settings) };
            var page5 = new MenuPageItem() { Title = "About", Icon = "round_help_black_24.xml", TargetType = typeof(About) };
            //var page6 = new MenuPageItem() { Title = "Google Login", Icon = "round_help_black_24.xml", TargetType = typeof(Pages.OAuthenticate) };


            ////Fot Ios icons
            //var page1 = new MasterPageItem() { Title = "FastDelivery", Icon = "ic_local_shipping.png", TargetType = typeof(View1) };
            //var page2 = new MasterPageItem() { Title = "Menus", Icon = "ic_restaurant.png", TargetType = typeof(View1) };
            //var page3 = new MasterPageItem() { Title = "Free Pizza", Icon = "ic_local_pizza.png", TargetType = typeof(View1) };
            //var page4 = new MasterPageItem() { Title = "Dining", Icon = "ic_local_dining.png", TargetType = typeof(View1) };
            //var page5 = new MasterPageItem() { Title = "Parking", Icon = "ic_local_parking.png", TargetType = typeof(View1) };
            //var page6 = new MasterPageItem() { Title = "LocateUs", Icon = "ic_my_location.png", TargetType = typeof(View1) };
            // Adding menu items to menuList
            menuList.Add(page1);
            menuList.Add(page2);
            //menuList.Add(page3);
            //menuList.Add(page4);
            menuList.Add(page5);
            //menuList.Add(page6);
            //menuList.Add(page6);


            // Setting our list to be ItemSource for ListView in MainPage.xaml
            navigationDrawerList.ItemsSource = menuList;
            // Initial navigation, this can be used for our home page
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(HomePage)));
            this.BindingContext = new
            {
                Header = "",
                Image = "Banner.png",
                Footer = "Footprint"
            };

        }

        //private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var item = e.SelectedItem as MenuPageMenuItem;
        //    if (item == null)
        //        return;

        //    var page = (Page)Activator.CreateInstance(item.TargetType);
        //    page.Title = item.Title;

        //    Detail = new NavigationPage(page);
        //    IsPresented = false;

        //    MasterPage.ListView.SelectedItem = null;
        //}
        //void Handle_Clicked(object sender, System.EventArgs e)
        //{
        //     Detail = new NavigationPage(new Home());
        //    //this.Navigation.PushAsync(new Home());
        //    IsPresented = false;
        //}

        void Handle_Clicked2(object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new About());
            IsPresented = false;
        }

        private void DBPage_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new DisplayData());
            IsPresented = false;
        }

        private void HomePage_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new HomePage());
            IsPresented = false;
        }

        private void Calculate_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Calculate());
            IsPresented = false;
        }

        private async void Settings_Clicked(object sender, EventArgs e)
        {
            var newPage = new Settings();
            await Navigation.PushAsync(newPage);
            (App.Current.MainPage as MasterDetailPage).IsPresented = true;
            //Detail = new NavigationPage(new Settings());
            //IsPresented = false;
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MenuPageItem)e.SelectedItem;
            Type page = item.TargetType;
            //Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            Detail.Navigation.PushAsync((Page)Activator.CreateInstance(page));
            IsPresented = false;
            item = null;
        }

        
    }
}