using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
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

        public static async Task<string> GetCoordinatesAsync(string address, string apiKey)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={apiKey}";

                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(responseBody);

                    if (json["status"].ToString() == "OK")
                    {
                        var location = json["results"][0]["geometry"]["location"];
                        double latitude = location["lat"].Value<double>();
                        double longitude = location["lng"].Value<double>();

                        return $"{latitude}, {longitude}";
                    }
                    else
                    {
                        return $"InvalidLocation";
                    }
                }
                catch(Exception ex)
                {
                    
                    return $"InvalidLocation";
                }
               
            }
        }
    }


}

