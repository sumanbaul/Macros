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
	public partial class About : ContentPage
	{
		public About ()
		{
			InitializeComponent ();

            var Names = new List<string>
            {
                "Dream","Travel","Time"
            };

            HomeCarouselView.ItemsSource = Names;


            var images = new List<string>
            {
                "https://78.media.tumblr.com/9e8ccd15f0a83dcb1e9f54d5849ed6a5/tumblr_p8ncl8WrPL1v2d0ejo1_1280.jpg",
                "https://78.media.tumblr.com/88302e4c6979cfcdaaa4e732564ffe18/tumblr_p8jfn5mfca1qevwuho1_500.jpg",
                "https://78.media.tumblr.com/c0f2739822e343c61c4ed8eb1bf9f631/tumblr_p8q9fhvnbd1xppqf9o1_1280.jpg"
            };

            HomeCarouselView.ItemsSource = images;

        }

        
	}
}