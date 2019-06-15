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
	public partial class Calculate : ContentPage
	{
		public Calculate ()
		{
			InitializeComponent ();
		}

        private async void  Calculate_Clicked(object sender, EventArgs e)
        {
            try
            {
                float activityLevel = 0;
                if (Activity.SelectedIndex == 0)
                {
                    activityLevel = (float)1.2;
                }
                else if (Activity.SelectedIndex == 1)
                {
                    activityLevel = (float)1.375;
                }
                else if (Activity.SelectedIndex == 2)
                {
                    activityLevel = (float)1.55;
                }
                else if (Activity.SelectedIndex == 3)
                {
                    activityLevel = (float)1.725;
                }
                else
                {
                    activityLevel = (float)1.9;
                }

                

                if (Weight.Text == null || Height.Text == null || Age.Text == null)
                {
                   await DisplayAlert("Failed", "No values were inserted. Enter values and try again.", "Close");
                }
                else if (float.Parse(Weight.Text) < 24 || float.Parse(Height.Text) < 3.5 || float.Parse(Age.Text) < 17)
                {
                    await DisplayAlert("Failed", "Inserted values are less than the minimum amount needed for survival.", "Close");
                }
                else
                {
                    Weight.Text = Weight.Text.Trim();
                    Height.Text = Height.Text.Trim();
                    Age.Text = Age.Text.Trim();

                    float HeightValue = 0;
                    float BMRValue;
                    float FatPercentage;

                    if (HeightO.Items[HeightO.SelectedIndex] == "Feet")
                    {
                        HeightValue = (float)(float.Parse(Height.Text) * 12); // in inches
                    }
                    else
                    {
                        HeightValue = (float)(float.Parse(Height.Text) * 0.39); // in inches
                    }



                    var Calculate = new BMR();

                    //weight = float.Parse(Weight.Text); //Kilos
                    Calculate.Weight = (float)(float.Parse(Weight.Text) * 2.2); //Lbs
                    Calculate.Height = HeightValue;
                    Calculate.Age = float.Parse(Age.Text);
                    Calculate.Date = DateTime.Today;
                    Calculate.ActivityLevels = activityLevel;
                    Calculate.BmiValue = (float)(Calculate.Weight / (Math.Pow(Calculate.Height, 2))) * 703;
                    

                    if (Gender.Items[Gender.SelectedIndex] == "Male")
                    {
                        //BMRValue = (float)( 10 * Bmr.weight + 6.25 * Bmr.height - 5 * Bmr.age + 5); //old
                        FatPercentage = (float)((1.20 * Calculate.BmiValue) + (0.23 * Calculate.Age) - 16.2);
                        BMRValue = (float)(66 + (Calculate.Weight * 6.23) + (12.7 * Calculate.Height) - (6.8 * Calculate.Age));
                    }
                    else
                    {
                        //BMRValue = (float)(10 * Bmr.weight + 6.25 * Bmr.height - 5 * Bmr.age) ;
                        FatPercentage = (float)((1.20 * Calculate.BmiValue) + (0.23 * Calculate.Age) - 5.4);
                        BMRValue = (float)(655 + (Calculate.Weight * 4.35) + (4.7 * Calculate.Height) - (4.7 * Calculate.Age));
                    }

                    Calculate.BmrValue = BMRValue;
                    Calculate.FatPercentageValue = FatPercentage;



                    ResetButton.IsVisible = true;

                    //sqlite
                    using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection((App.DB_PATH)))
                    {
                        connection.CreateTable<BMR>();
                        var NumberOfRows = connection.Insert(Calculate);

                        if (NumberOfRows > 0)
                        {
                            //DisplayAlert("Success", "DB Insertion successful", "exit");
                            var newPage = new HomePage();
                            await Navigation.PushAsync(newPage);
                        }
                        else
                        {
                           await DisplayAlert("failure", "DB insertion failed", "exit");
                        }
                    }

                    //sqlite
                }


            }

            catch (Exception ex)
            {
                await DisplayAlert("Failed", ex.ToString(), "Close");
            }
        }

        private void ResetButton_Clicked(object sender, EventArgs e)
        {
            

            Gender.Items.Clear();
            Weight.Text = "";

            HeightO.Items.Clear();
            Height.Text = "";

            Age.Text = "";
        }

        
    }
}