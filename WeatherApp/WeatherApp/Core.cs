using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherApp
{
    public class Core
    {
        public static async Task<Weather> GetWeather(string zipcode)
        {
            // Load the API Key from a configuration file
            Conf conf = JsonConvert.DeserializeObject<Conf>(File.ReadAllText("conf.json"));

            // Create the request to send to the server
            string queryString = "http://api.openweathermap.org/data/2.5/weather?zip=" +
                zipcode + ",us&appid=" + conf.APIKey + "&units=imperial";

            // Send the request to the server
            dynamic results = await DataService.getDataFromService(queryString).ConfigureAwait(false);

            // If a response was sent back, receive the data
            if (results["weather"] != null)
            {
                Weather weather = new Weather();
                weather.Title = (string)results["name"];
                weather.Temperature = (string)results["main"]["temp"] + " F";
                weather.Wind = (string)results["wind"]["speed"] + " mph";
                weather.Humidity = (string)results["main"]["humidity"] + " %";
                weather.Visibility = (string)results["weather"][0]["main"];

                DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                DateTime sunrise = time.AddSeconds((double)results["sys"]["sunrise"]);
                DateTime sunset = time.AddSeconds((double)results["sys"]["sunrise"]);
                weather.Sunrise = sunrise.ToString() + " UTC";
                weather.Sunset = sunset.ToString() + " UTC";

                return weather;
            }
            else
            {
                return null;
            }
        }
    }

    class Conf
    {
        public string APIKey { get; set; }
    }
}
