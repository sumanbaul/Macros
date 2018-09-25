using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Entry = Microcharts.Entry;
using Microcharts;

namespace Macros
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DisplayData : ContentPage
	{



        //List<Entry> entries = new List<Entry>()
        //{
            

        //    new Entry(200)
        //    {
        //        Color = SKColor.Parse("#ff1493"),
        //        Label = "January",
        //        ValueLabel = "200"
        //    },
        //    new Entry(400)
        //    {
        //        Color = SKColor.Parse("#00BFFF"),
        //        Label = "March",
        //        ValueLabel = "400"
        //    },
        //    new Entry(-100)
        //    {
        //        Color = SKColor.Parse("#00CDE1"),
        //        Label = "April",
        //        ValueLabel = "-100"
        //    }
        //};


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
                //BMRView.ItemsSource = bmr;
                
                List<Entry> entries = new List<Entry>();
                //int count = 0;
                foreach (var element in bmr)
                {
                    Entry data = new Entry(element.bmiValue);
                    data.ValueLabel = element.bmrValue.ToString("0");
                    data.Label = element.Date.ToString("MMMM");
                    data.Color = SKColor.Parse("#00CDE1");
                    entries.Add(data);

                    //entries[bmr.Count] = new Entry(bmr.Count) {
                    //    Color = SKColor.Parse("#00CDE1"),
                    //    Label = "April",
                    //    ValueLabel = bmr[bmr.Count].bmrValue.ToString()
                    //};

                    BMRView.ItemsSource = bmr;

                    


                }
                DataChart.Chart = new LineChart { Entries = entries };
                //string strserialize = JsonConvert.SerializeObject(bmr);
            };
        }
    }
}