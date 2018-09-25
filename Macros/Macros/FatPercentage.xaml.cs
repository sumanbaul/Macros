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
	public partial class FatPercentage : ContentPage
	{
		public FatPercentage ()
		{
			InitializeComponent ();
		}

        private void FatPercentage_Clicked(object sender, EventArgs e)
        {
            try
            {


                if (Weight.Text == null || Height.Text == null || Age.Text == null)
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
                    //float BMIValue;
                    float FatPercentage;

                    if (HeightO.Items[HeightO.SelectedIndex] == "Feet")
                    {
                        HeightValue = (float)(float.Parse(Height.Text) * 12); // in inches
                    }
                    else
                    {
                        HeightValue = (float)(float.Parse(Height.Text) * 0.39); // in inches
                    }



                    var Bmi = new BMR();

                    //weight = float.Parse(Weight.Text); //Kilos
                    Bmi.weight = (float)(float.Parse(Weight.Text) * 2.2); //Lbs
                    Bmi.height = HeightValue;
                    Bmi.age = float.Parse(Age.Text);

                    Bmi.bmiValue = (float)(Bmi.weight / (Math.Pow(Bmi.height,2)))*703;

                    if (Gender.Items[Gender.SelectedIndex] == "Male")
                    {
                        //BMRValue = (float)( 10 * Bmr.weight + 6.25 * Bmr.height - 5 * Bmr.age + 5); //old
                        FatPercentage = (float)((1.20 * Bmi.bmiValue) + (0.23 * Bmi.age) - 16.2);
                    }
                    else
                    {
                        //BMRValue = (float)(10 * Bmr.weight + 6.25 * Bmr.height - 5 * Bmr.age) ;
                        FatPercentage = (float)((1.20 * Bmi.bmiValue) + (0.23 * Bmi.age) - 5.4);
                    }

                    Bmi.FatPercentageValue = FatPercentage;

                    FatPercentageResult.Text = "Your Fat % is " + FatPercentage.ToString("0.00");
                    FatPercentageResult.IsVisible = true;
                    FatPercentageResult.FadeTo(1, 1000);
                    // ResetButton.IsVisible = true;

                    //sqlite
                    using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection((App.DB_PATH)))
                    {
                        connection.CreateTable<BMR>();
                        var NumberOfRows = connection.Insert(Bmi);

                        if (NumberOfRows > 0)
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

            catch (Exception ex)
            {
                DisplayAlert("Failed", ex.ToString(), "Close");
            }

        }
    }
}