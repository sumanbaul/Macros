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
    public partial class Home : ContentPage
    {
        public Home()
        {
            
            InitializeComponent();
        }

        private void CalculateBMR(object sender, EventArgs e)
        {
            try
            {
               

                if(Weight.Text == null || Height.Text == null || Age.Text == null)
                {
                    DisplayAlert("Failed", "No values were inserted. Enter values and try again.", "Close");
                }
                else if (float.Parse(Weight.Text) < 24 || float.Parse(Height.Text) < 3.5 || float.Parse(Age.Text) < 17)
                {
                    DisplayAlert("Failed", "Inserted values are less than the minimum amount needed for survival.", "Close");
                }
                else
                {
                    Weight.Text = Weight.Text.Trim();
                    Height.Text = Height.Text.Trim();
                    Age.Text = Age.Text.Trim();
                    //if(Gender.SelectedIndex.)

                    float HeightValue = 0;
                    float BMRValue;

                    if (HeightO.Items[HeightO.SelectedIndex] == "Feet")
                    {
                        //HeightValue = (float)(float.Parse(HeightSplitArray[0])*30.48) + (float)(float.Parse(HeightSplitArray[1]) * 2.54); // in cms
                        HeightValue = (float)(float.Parse(Height.Text) * 12); // in inches
                    }
                    else
                    {
                        HeightValue = (float)(float.Parse(Height.Text) * 0.39); // in inches
                    }



                    var Bmr = new BMR();

                    //weight = float.Parse(Weight.Text); //Kilos
                    Bmr.Weight = (float)(float.Parse(Weight.Text) * 2.2); //Lbs
                      Bmr.Height = HeightValue;
                      Bmr.Age = float.Parse(Age.Text);
                     
                    

                    if (Gender.Items[Gender.SelectedIndex] == "Male")
                    {
                        //BMRValue = (float)( 10 * Bmr.weight + 6.25 * Bmr.height - 5 * Bmr.age + 5); //old
                        BMRValue = (float)(66 + (Bmr.Weight * 6.23) + (12.7 * Bmr.Height) - (6.8 * Bmr.Age));
                    }
                    else
                    {
                        //BMRValue = (float)(10 * Bmr.weight + 6.25 * Bmr.height - 5 * Bmr.age) ;
                        BMRValue = (float)(655 + (Bmr.Weight * 4.35) + (4.7 * Bmr.Height) - (4.7 * Bmr.Age));
                    }
                    Bmr.BmrValue = BMRValue;

                    BMRResult.Text = "Your BMR value is " + BMRValue.ToString("0.00");
                    BMRResult.IsVisible = true;
                    BMRResult.FadeTo(1, 1000);
                    ResetButton.IsVisible = true;

                    //sqlite
                    using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection((App.DB_PATH)))
                    {
                        connection.CreateTable<BMR>();
                        var NumberOfRows =  connection.Insert(Bmr);

                        if(NumberOfRows > 0)
                        {
                            DisplayAlert("Success", "DB Insertion successful", "exit");
                        }
                        else
                        {
                            DisplayAlert("failure", "DB insertion failed", "exit");
                        }
                    }

                    //sqlite
                }


            }

            catch(Exception ex)
            {
                DisplayAlert("Failed", ex.ToString(), "Close");
            }

            
        }

        private void Reset(object sender, EventArgs e)
        {
            BMRResult.IsVisible = false;
            BMRResult.FadeTo(0, 250);

            Gender.Items.Clear();
            Weight.Text = "";

            HeightO.Items.Clear();
            Height.Text = "";

            Age.Text = "";
        }

        //protected override bool OnBackButtonPressed()
        //{
        //    Device.BeginInvokeOnMainThread(async () =>
        //    {
        //        var result = await this.DisplayAlert("Alert!", "Do you want to exit?", "Yes", "No");
        //        if (result) await this.Navigation.PopAsync();
        //    });

        //    return true;
        //}
    }
}