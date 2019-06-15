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
	public partial class Macros : ContentPage
	{
		public Macros ()
		{
			InitializeComponent ();

            GainWeight.HeightRequest = 30;
            LoseWeight.HeightRequest = 30;
            MaintainWeight.HeightRequest = 50;

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
            {
                int goal = 0;
                conn.CreateTable<BMR>();
                var bmr = conn.Table<BMR>().ToList();
                float weight = 0;
                float activityLvl = 0;
                float BMRVal = 0;
                foreach (BMR data in bmr)
                {
                    weight = data.Weight;
                    //BMIVal = fat.bmiValue.ToString("0.00");
                    BMRVal = data.BmrValue;
                    activityLvl = data.ActivityLevels;
                }

                
                var MacroData = CalculateMacros(weight, BMRVal, activityLvl, goal);
                labelProtein.Text = MacroData[0].ToString("0");
                labelFat.Text = MacroData[1].ToString("0");
                labelCarbs.Text = MacroData[2].ToString("0");

                
                //string strserialize = JsonConvert.SerializeObject(bmr);
            };
        }

        //method calculation of macros
        public float[] CalculateMacros(float bodyWeight, float bmrvalue, float activityLevel, int desiredGoal)
        {
            float[] macros=new float[3];
            float protein, fat, carbs = 0;
            float calories = bmrvalue * activityLevel;
            //float goalMaintainence, goalMuscleGain, goalFatLoss = 0;

            if (desiredGoal == 0) //maintainance
            {
                
                protein = (float)(0.825 * bodyWeight);
                fat = (float)((0.25 * calories) / 9);
                carbs = (float)(calories - ((protein * 4) + (fat * 9)))/4;
                macros[0] = protein;
                macros[1] = fat;
                macros[2] = carbs;
            }
            else if (desiredGoal == 1) //Gain weight
            {
                calories = calories + (float)(calories * 0.20);
                protein = (float)(1.525 * bodyWeight);
                fat = (float)((0.25 * calories) / 9);
                carbs = (float)(calories - ((protein * 4) + (fat * 9)))/4;
                macros[0] = protein;
                macros[1] = fat;
                macros[2] = carbs;
            }

            else if (desiredGoal == 2) //lose weight
            {
                calories = calories - (float)(calories * 0.20);
                protein = (float)(0.825 * bodyWeight);
                fat = (float)((0.25 * calories) / 9);
                carbs = (float)(calories - ((protein * 4) + (fat * 9)))/4;
                macros[0] = protein;
                macros[1] = fat;
                macros[2] = carbs;
            }



            return macros;
        }

        private void btnLoseWeight(object sender, EventArgs e)
        {
            //GainWeight.HeightRequest = 30;
            //MaintainWeight.HeightRequest = 30;
            //this.HeightRequest = 50;
            GainWeight.FontSize = 12;
            GainWeight.FontFamily = "TitilliumWeb-Regular.ttf#TitilliumWeb-Regular";
            LoseWeight.FontSize = 20;
            LoseWeight.FontFamily = "TitilliumWeb-SemiBold.ttf#TitilliumWeb-SemiBold";
            MaintainWeight.FontSize = 12;
            MaintainWeight.FontFamily = "TitilliumWeb-Regular.ttf#TitilliumWeb-Regular";

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
            {
                int goal = 2;
                conn.CreateTable<BMR>();
                var bmr = conn.Table<BMR>().ToList();
                float weight = 0;
                float activityLvl = 0;
                float BMRVal = 0;
                foreach (BMR data in bmr)
                {
                    weight = data.Weight;
                    //BMIVal = fat.bmiValue.ToString("0.00");
                    BMRVal = data.BmrValue;
                    activityLvl = data.ActivityLevels;
                }


                var MacroData = CalculateMacros(weight, BMRVal, activityLvl, goal);
                labelProtein.Text = MacroData[0].ToString("0");
                labelFat.Text = MacroData[1].ToString("0");
                labelCarbs.Text = MacroData[2].ToString("0");
                //string strserialize = JsonConvert.SerializeObject(bmr);
            };

        }

        private void btnGainWeight(object sender, EventArgs e)
        {
            //MaintainWeight.HeightRequest = 30;
            //LoseWeight.HeightRequest = 30;
            //this.HeightRequest = 50;

            GainWeight.FontSize = 20;
            GainWeight.FontFamily = "TitilliumWeb-SemiBold.ttf#TitilliumWeb-SemiBold"; ;
            LoseWeight.FontSize = 12;
            LoseWeight.FontFamily = "TitilliumWeb-Regular.ttf#TitilliumWeb-Regular";
            MaintainWeight.FontSize = 12;
            MaintainWeight.FontFamily = "TitilliumWeb-Regular.ttf#TitilliumWeb-Regular";

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
            {
                int goal = 1;
                conn.CreateTable<BMR>();
                var bmr = conn.Table<BMR>().ToList();
                float weight = 0;
                float activityLvl = 0;
                float BMRVal = 0;
                foreach (BMR data in bmr)
                {
                    weight = data.Weight;
                    //BMIVal = fat.bmiValue.ToString("0.00");
                    BMRVal = data.BmrValue;
                    activityLvl = data.ActivityLevels;
                }


                var MacroData = CalculateMacros(weight, BMRVal, activityLvl, goal);
                labelProtein.Text = MacroData[0].ToString("0");
                labelFat.Text = MacroData[1].ToString("0");
                labelCarbs.Text = MacroData[2].ToString("0");
                //string strserialize = JsonConvert.SerializeObject(bmr);
            };
        }

        private void btnMaintainWeight(object sender, EventArgs e)
        {
            //GainWeight.HeightRequest = 30;
            //LoseWeight.HeightRequest = 30;
            //this.HeightRequest = 50;
            GainWeight.FontSize = 12;
            GainWeight.FontFamily = "TitilliumWeb-Regular.ttf#TitilliumWeb-Regular";
            MaintainWeight.FontSize = 20;
            MaintainWeight.FontFamily = "TitilliumWeb-SemiBold.ttf#TitilliumWeb-SemiBold";
            LoseWeight.FontSize = 12;
            LoseWeight.FontFamily = "TitilliumWeb-Regular.ttf#TitilliumWeb-Regular";

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
            {
                int goal = 0;
                conn.CreateTable<BMR>();
                var bmr = conn.Table<BMR>().ToList();
                float weight = 0;
                float activityLvl = 0;
                float BMRVal = 0;
                foreach (BMR data in bmr)
                {
                    weight = data.Weight;
                    //BMIVal = fat.bmiValue.ToString("0.00");
                    BMRVal = data.BmrValue;
                    activityLvl = data.ActivityLevels;
                }


                var MacroData = CalculateMacros(weight, BMRVal, activityLvl, goal);
                labelProtein.Text = MacroData[0].ToString("0");
                labelFat.Text = MacroData[1].ToString("0");
                labelCarbs.Text = MacroData[2].ToString("0");
                //string strserialize = JsonConvert.SerializeObject(bmr);
            };
        }
    }
}