using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Macros.Controls;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Macros
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        SKPaintSurfaceEventArgs args;
        ProgressUtils progressUtils = new ProgressUtils();

        public HomePage()
        {
            InitializeComponent();
            InitiateProgressUpdate();
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


        // Initializing the canvas & drawing over it
        // Check here https://stackoverflow.com/questions/52893416/xamarin-forms-async-task-signature-return-type-of-eventhandler-doesnt-matc
        async void OnCanvasViewPaintSurfaceAsync(object sender, SKPaintSurfaceEventArgs args1)
        {
            args = args1;
            await drawGaugeAsync();

        }


        void sliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            if (canvas != null)
            {
                // Invalidating surface due to data change
                canvas.InvalidateSurface();
            }
        }

        // Animating the Progress of Radial Gauge
        async void animateProgress(int progress)
        {
            
            sweepAngleSlider.Value = 1;

            // Looping at data interval of 5
            for (int i = 0; i < progress; i = i + 5)
            {
                sweepAngleSlider.Value = i;
                await Task.Delay(3);
            }

            sweepAngleSlider.Value = progress;
            //sw_listToggle.IsEnabled = true;

        }

        private void InitiateProgressUpdate()
        {
            //throw new NotImplementedException();
            animateProgress(progressUtils.getSweepAngle(900, 300));
        }



        
        public async Task drawGaugeAsync()
        {
            // Radial Gauge Constants
            int uPadding = 150;
            int side = 500;
            int radialGaugeWidth = 20;

            // Line TextSize inside Radial Gauge
            int lineSize1 = 220;
            int lineSize2 = 70;
            int lineSize3 = 80;

            // Line Y Coordinate inside Radial Gauge
            int lineHeight1 = 100;
            int lineHeight2 = 200;
            int lineHeight3 = 300;

            // Start & End Angle for Radial Gauge
            float startAngle = -220;
            float sweepAngle = 260;

            try
            {

                // Getting Canvas Info 
                SKImageInfo info = args.Info;
                SKSurface surface = args.Surface;
                SKCanvas canvas = surface.Canvas;
                SKCanvas canvas2 = surface.Canvas;
                progressUtils.setDevice(info.Height, info.Width);
                canvas.Clear();

                // Getting Device Specific Screen Values
                // -------------------------------------------------

                // Top Padding for Radial Gauge
                float upperPading = progressUtils.getFactoredHeight(uPadding);

                /* Coordinate Plotting for Radial Gauge
                *
                *    (X1,Y1) ------------
                *           |   (XC,YC)  |
                *           |      .     |
                *         Y |            |
                *           |            |
                *            ------------ (X2,Y2))
                *                  X
                *   
                *To fit a perfect Circle inside --> X==Y
                *       i.e It should be a Square
                */

                // Xc & Yc are center of the Circle
                int Xc = info.Width / 2;
                float Yc = progressUtils.getFactoredHeight(side);

                // X1 Y1 are lefttop cordiates of rectange
                int X1 = (int)(Xc - Yc);
                int Y1 = (int)(Yc - Yc + upperPading);

                // X2 Y2 are rightbottom cordiates of rectange
                int X2 = (int)(Xc + Yc);
                int Y2 = (int)(Yc + Yc + upperPading);

                //Loggig Screen Specific Calculated Values
                Debug.WriteLine("INFO " + info.Width + " - " + info.Height);
                Debug.WriteLine(" C : " + upperPading + "  " + info.Height);
                Debug.WriteLine(" C : " + Xc + "  " + Yc);
                Debug.WriteLine("XY : " + X1 + "  " + Y1);
                Debug.WriteLine("XY : " + X2 + "  " + Y2);

                //  Empty Gauge Styling
                SKPaint paint1 = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = Color.FromHex("#ffffff").ToSKColor(),                   // Colour of Radial Gauge
                    StrokeWidth = progressUtils.getFactoredWidth(radialGaugeWidth), // Width of Radial Gauge
                    StrokeCap = SKStrokeCap.Round                                   // Round Corners for Radial Gauge
                };

                // Filled Gauge Styling
                SKPaint paint2 = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = Color.FromHex("#05c7c7").ToSKColor(),                   // Overlay Colour of Radial Gauge
                    StrokeWidth = progressUtils.getFactoredWidth(radialGaugeWidth), // Overlay Width of Radial Gauge
                    StrokeCap = SKStrokeCap.Round                                   // Round Corners for Radial Gauge
                };

                // Defining boundaries for Gauge
                SKRect rect = new SKRect(X1, Y1, X2, Y2);


                //canvas.DrawRect(rect, paint1);
                //canvas.DrawOval(rect, paint1);

                // Rendering Empty Gauge
                SKPath path1 = new SKPath();
                path1.AddArc(rect, startAngle, sweepAngle);
                canvas.DrawPath(path1, paint1);

                // Rendering Filled Gauge
                SKPath path2 = new SKPath();
                path2.AddArc(rect, startAngle, (float)sweepAngleSlider.Value);
                canvas.DrawPath(path2, paint2);

                //---------------- Drawing Text Over Gauge ---------------------------

                // Achieved Minutes
                using (SKPaint skPaint = new SKPaint())
                {
                    skPaint.Style = SKPaintStyle.Fill;
                    skPaint.IsAntialias = true;
                    skPaint.Color = SKColor.Parse("#676a69");
                    skPaint.TextAlign = SKTextAlign.Center;
                    skPaint.TextSize = progressUtils.getFactoredHeight(lineSize1);
                    skPaint.Typeface = SKTypeface.FromFamilyName(
                                        "Arial",
                                        SKFontStyleWeight.Bold,
                                        SKFontStyleWidth.Normal,
                                        SKFontStyleSlant.Upright);

                    // Drawing Achieved Minutes Over Radial Gauge
                    
                        canvas.DrawText(100 + "", Xc, Yc + progressUtils.getFactoredHeight(lineHeight1), skPaint);
                    canvas2.DrawText(100 + "", Xc, Yc + progressUtils.getFactoredHeight(lineHeight1), skPaint);
                }

                // Achieved Minutes Text Styling
                //using (SKPaint skPaint = new SKPaint())
                //{
                //    skPaint.Style = SKPaintStyle.Fill;
                //    skPaint.IsAntialias = true;
                //    skPaint.Color = SKColor.Parse("#676a69");
                //    skPaint.TextAlign = SKTextAlign.Center;
                //    skPaint.TextSize = progressUtils.getFactoredHeight(lineSize2);
                //    canvas.DrawText("Minutes", Xc, Yc + progressUtils.getFactoredHeight(lineHeight2), skPaint);
                //}

                //// Goal Minutes Text Styling
                //using (SKPaint skPaint = new SKPaint())
                //{
                //    skPaint.Style = SKPaintStyle.Fill;
                //    skPaint.IsAntialias = true;
                //    skPaint.Color = SKColor.Parse("#e2797a");
                //    skPaint.TextAlign = SKTextAlign.Center;
                //    skPaint.TextSize = progressUtils.getFactoredHeight(lineSize3);

                //    // Drawing Text Over Radial Gauge
                //    if (sw_listToggle.IsToggled)
                //        canvas.DrawText("Goal " + goal + " Min", Xc, Yc + progressUtils.getFactoredHeight(lineHeight3), skPaint);
                //    else
                //    {
                //        canvas.DrawText("Goal " + goal / 30 + " Min", Xc, Yc + progressUtils.getFactoredHeight(lineHeight3), skPaint);
                //    }
                //}

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
        }
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