using Newtonsoft.Json;
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
	public partial class DisplayData : ContentPage
	{
		public DisplayData ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
            {
                conn.CreateTable<BMR>();
                var bmr = conn.Table<BMR>().ToList();
                BMRView.ItemsSource = bmr;

                string strserialize = JsonConvert.SerializeObject(bmr);
            };
        }
    }
}