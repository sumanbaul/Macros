using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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

        public string UserWeight { get; set; }
        public string UserDate { get; set; }

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
                
                
                List<Microcharts.ChartEntry> entries = new List<Microcharts.ChartEntry>();
                //string[] color = { "#00CDE1", "#C71585", "f2db68", "f2db68" };
                //int count = 0;
                var random = new Random();
                var color = "";
                
                foreach (var element in bmr)
                {
                    Microcharts.ChartEntry data = new Microcharts.ChartEntry(element.BmiValue);
                    data.ValueLabel = element.BmrValue.ToString("0");
                    data.Label = element.Date.Date.ToString("MMMM");
                    data.Color = SKColor.Parse(color = String.Format("#{0:X6}", random.Next(0x1000000)));
                    entries.Add(data);
                    element.FatPercentageValue.ToString("0.00 %");
                    //entries[bmr.Count] = new Entry(bmr.Count) {
                    //    Color = SKColor.Parse("#00CDE1"),
                    //    Label = "April",
                    //    ValueLabel = bmr[bmr.Count].bmrValue.ToString()
                    //};

                    BMRView.ItemsSource = bmr;
                    UserWeight = element.Weight.ToString();
                    UserDate = element.Date.Date.ToString("DD/MM/YY");
                    
                    
                }
                DataChart.Chart = new DonutChart { Entries = entries };
                //string strserialize = JsonConvert.SerializeObject(bmr);
            };
        }
    }
}