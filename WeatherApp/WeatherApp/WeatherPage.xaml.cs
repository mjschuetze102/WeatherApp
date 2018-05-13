using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WeatherPage : ContentPage
	{
		public WeatherPage ()
		{
			InitializeComponent ();

            // Set the default binding to a default object for now
            BindingContext = new Weather();
		}

        private async void GetWeatherBtn_Clicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(zipCodeEntry.Text))
            {
                // Get the weather at the specified zip code
                Weather weather = await Core.GetWeather(zipCodeEntry.Text);
                BindingContext = weather;

                getWeatherBtn.Text = "Search Again";
            }
        }
	}
}