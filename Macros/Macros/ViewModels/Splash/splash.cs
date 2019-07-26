using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Macros.Splash
{
    public class splash : ContentPage
    {
        Image splashImage;

        public splash()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            var sub = new AbsoluteLayout();
            splashImage = new Image
            {
                Source = "icon.png",
                WidthRequest = 100,
                HeightRequest = 100
            };

            AbsoluteLayout.SetLayoutFlags(splashImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            sub.Children.Add(splashImage);

            this.BackgroundColor = Color.FromHex("#fff"); //#25232e
            this.Content = sub;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await splashImage.ScaleTo(1, 1000);
            await splashImage.ScaleTo(0.7, 1500, Easing.Linear);
            await splashImage.ScaleTo(1, 1000, Easing.Linear);
            await splashImage.ScaleTo(0.8, 1200, Easing.Linear);
            Application.Current.MainPage = new MenuPage();

        }

    }
}
