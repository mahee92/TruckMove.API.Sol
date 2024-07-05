using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Helper
{
    public static class GoogleMapsHelper
    {

        public static async Task<string> GetDistanceAsync(string startLocation, string endLocation, string apiKey)
        {
            string requestUrl = $"https://maps.googleapis.com/maps/api/directions/json?origin={startLocation}&destination={endLocation}&key={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(responseBody);

                var route = jsonResponse["routes"][0];
                var leg = route["legs"][0];
                var distance = leg["distance"]["text"];

                return (string?)distance;
            }
        }
        public static async Task<string> ValidateLocations(string startLocation, string endLocation, string apiKey)
        {
            string requestUrl = $"https://maps.googleapis.com/maps/api/directions/json?origin={startLocation}&destination={endLocation}&key={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(responseBody);

                var status = jsonResponse["status"];

                return (string?)status;
            }
        }


    }
}
