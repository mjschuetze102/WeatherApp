using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherApp
{
    public class DataService
    {
        public static async Task<dynamic> getDataFromService(string queryString)
        {
            // Wait for a response from the server
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(queryString);

            // Receive the data from the server response
            dynamic data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }

            return data;
        }
    }
}
