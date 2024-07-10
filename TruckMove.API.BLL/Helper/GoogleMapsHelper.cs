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

        public static async Task<decimal> GetDistanceAsync(string startLocation, string endLocation, string apiKey)
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
               // string distance = leg["distance"]["text"].ToString();
                int distance = Convert.ToInt32(leg["distance"]["value"].ToString());

                // Convert distance1 to a decimal before division
                decimal distanceInKilometers = (decimal)distance / 1000;

                // Round to 2 decimal places (if necessary)
                distanceInKilometers = Math.Round(distanceInKilometers, 2);

                // Step 4: Convert back to string (if needed)
             


                

                return distanceInKilometers;
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
