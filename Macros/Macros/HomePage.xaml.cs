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
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();

            var Names = new List<string>
            {
                "Fat Percentage: Healthy","Travel","Time"
            };

            HomePageCarousel.ItemsSource = Names;


        }



        private async void FatPercentage_Tapped(object sender, EventArgs e)
        {
            var newPage = new FatPercentage();
            await Navigation.PushAsync(newPage);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
            {
                conn.CreateTable<BMR>();
                var bmr = conn.Table<BMR>().ToList();
                var FatPercentageVal="";
                var BMIVal = "";
                var BMRVal = "";
                foreach(BMR fat in bmr)
                {
                    FatPercentageVal = fat.FatPercentageValue.ToString("0.00");
                    BMIVal = fat.bmiValue.ToString("0.00");
                    BMRVal = fat.bmrValue.ToString("0.00");
                }
                FPer.Text = FatPercentageVal + " %";
                BMIResult.Text = BMIVal;
                BMRResult.Text = BMRVal;

                //string strserialize = JsonConvert.SerializeObject(bmr);
            };
        }

        private async void Calculate_Tapped(object sender, EventArgs e)
        {
            var newPage = new Calculate();
            await Navigation.PushAsync(newPage);
        }

        private async void Macros_Tapped(object sender, EventArgs e)
        {
            var newPage = new Macros();
            await Navigation.PushAsync(newPage);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}