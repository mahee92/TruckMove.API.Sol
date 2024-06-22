using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public static class JobApiClient
{
    private static readonly HttpClient _httpClient;
    private static readonly string _baseAddress = "https://vtmtruckmove.api.dev.riverina.digital:444";

    static JobApiClient()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(_baseAddress);
    }

    public static void SetJwtToken(string jwtToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
    }

    public static async Task<string> ApiCallAsync()
    {
        // Construct the API endpoint URL
        string endpoint = "/Odata/Mobile/Job/Get?$filter=status eq 3 or status eq 4 or status eq 5 or status eq 6 or status eq 7 or status eq 8 or status eq 9&orderby=Status desc,PickupDate desc&$top=5&$skip=5&$expand=VehicleNavigation";

        try
        {
            // Make the API call
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            // Ensure the response is successful
            response.EnsureSuccessStatusCode();

            // Read the response content
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }
        catch (HttpRequestException e)
        {
            // Handle potential errors
            Console.WriteLine($"Request error: {e.Message}");
            return null;
        }
    }
}
