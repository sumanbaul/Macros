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
        public MenuPage()
        {
            InitializeComponent();

            Detail = new NavigationPage(new Home());

            IsPresented = false;
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
        void Handle_Clicked(object sender, System.EventArgs e)
        {
             Detail = new NavigationPage(new Home());
            //this.Navigation.PushAsync(new Home());
            IsPresented = false;
        }

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
    }
}