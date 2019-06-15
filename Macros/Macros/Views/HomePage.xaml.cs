using System;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Macros
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();


        }



        private async void FatPercentage_Tapped(object sender, EventArgs e)
        {
            var newPage = new FatPercentage();
            await Navigation.PushAsync(newPage);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            string FatPercentage = "";
            string BMIValue = "";
            string BMRValue = "";

            try
            {
                BMIResult.Text = "";
                BMRResult.Text = "";
                FPer.Text = "";
                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
                {
                    conn.CreateTable<BMR>();
                    var bmr = conn.Table<BMR>().ToList();
                    var FatPercentageVal = "";
                    var BMIVal = "";
                    var BMRVal = "";
                    foreach (BMR fat in bmr)
                    {
                        FatPercentageVal = fat.FatPercentageValue.ToString("0.00");
                        BMIVal = fat.BmiValue.ToString("0.00");
                        BMRVal = fat.BmrValue.ToString("0.00");
                    }
                    FPer.Text = FatPercentageVal + " %";

                    BMIResult.Text = BMIVal;
                    BMRResult.Text = BMRVal;

                    FatPercentage = FatPercentageVal + " %";


                    BMIValue = BMIVal;
                    BMRValue = BMRVal;

                    //string strserialize = JsonConvert.SerializeObject(bmr);
                };

                //var Names = new List<string>
                //{
                //    "Fat Percentage: " + FatPercentage,"Your BMI Value: " + BMIValue,"Your BMR: " + BMRValue
                //};

                //Carousel.ItemsSource = Names;

                var YourObservableCollection = new ObservableCollection<string> { "Fat Percentage: " + FatPercentage, "Your BMI Value: " + BMIValue, "Your BMR: " + BMRValue };
                Carousel.ItemsSource = YourObservableCollection;
                Carousel.ShowIndicators = true;


            }
            catch (Exception ex)
            {
                DisplayAlert("Some Error Occured", ex.ToString(), "Close");
            }



            //if (BMIResult.Text == "" || FPer.Text == "" || BMRResult.Text == "")
            //{
            //    CalculateBtnView.IsVisible = true;
            //    DashBoardView.IsVisible = false;
            //}
            //else
            //{
            //    DashBoardView.IsVisible = true;
            //    CalculateBtnView.IsVisible = false;
            //}

            //DashBoardView.IsVisible = false;


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

        //private async void Calculate_Clicked(object sender, EventArgs e)
        //{
        //    var newPage = new Calculate();
        //    await Navigation.PushAsync(newPage);
        //}


    }

    //public class Person
    //{
    //    public Person()
    //    {
    //    }

    //    public string PhotoUrl { get; set; }
    //    public string Bio { get; set; }
    //    public string ContactInfo { get; set; }
    //}


}