using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using TruckMove.API.Helper;
using TruckMove.API.BLL.Helper;

namespace TruckMove.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class XeroAuthController : Controller
    {
        private readonly IAuthUserService _authUserService;
        private readonly XeroConfiguration _xeroConfig;
        private readonly IHttpClientFactory _httpClientFactory;

        public XeroAuthController(
            IAuthUserService authUserService,
            IOptions<XeroConfiguration> xeroConfig,
            IHttpClientFactory httpClientFactory)
        {
            _authUserService = authUserService;
            _xeroConfig = xeroConfig.Value;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("redirect")]
        public async Task<IActionResult> RedirectToXero()
        {
            if (IsAccessTokenValid())
            {
                if (IsTokenExpired())
                {
                    await RefreshTokenAsync();
                }
                return StatusCode((int)ErrorCode.alreadyExists, "Already Logged In");
            }

            var state = Guid.NewGuid().ToString(); // Prevent CSRF attacks
            var authorizationUrl = $"https://login.xero.com/identity/connect/authorize" +
                                   $"?client_id={_xeroConfig.ClientId}" +
                                   $"&redirect_uri={_xeroConfig.CallbackUri}" +
                                   $"&response_type=code" +
                                   $"&scope=openid profile email offline_access accounting.transactions" +
                                   $"&state={state}";

            Log.Information("Xero Authorization URL generated.");
            return Ok(authorizationUrl);
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback(string code, string state)
        {
            if (string.IsNullOrEmpty(code))
            {
                Log.Warning("Authorization code is missing.");
                return BadRequest("Authorization code is required.");
            }

            var client = _httpClientFactory.CreateClient();
            var requestData = new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", code },
                { "redirect_uri", _xeroConfig.CallbackUri },
                { "client_id", _xeroConfig.ClientId },
                { "client_secret", _xeroConfig.ClientSecret }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://identity.xero.com/connect/token")
            {
                Content = new FormUrlEncodedContent(requestData)
            };

            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Log.Error($"Xero token request failed: {responseContent}");
                return StatusCode((int)response.StatusCode, "Failed to get token from Xero.");
            }

            var tokenResponse = JsonSerializer.Deserialize<XeroTokenResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            SetSessionToken(tokenResponse.Access_Token, tokenResponse.Refresh_Token, tokenResponse.Expires_In);

            Log.Information("Xero token received and stored.");
            return Ok("Authorization successful.");
        }

        private async Task RefreshTokenAsync()
        {
            var refreshToken = HttpContext.Session.GetString("RefreshToken");
            if (string.IsNullOrEmpty(refreshToken))
            {
                Log.Warning("No refresh token found.");
                return;
            }

            var client = _httpClientFactory.CreateClient();
            var requestData = new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "refresh_token", refreshToken },
                { "client_id", _xeroConfig.ClientId },
                { "client_secret", _xeroConfig.ClientSecret }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://identity.xero.com/connect/token")
            {
                Content = new FormUrlEncodedContent(requestData)
            };

            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Log.Error($"Xero token refresh failed: {responseContent}");
                return;
            }

            var tokenResponse = JsonSerializer.Deserialize<XeroTokenResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            SetSessionToken(tokenResponse.Access_Token, tokenResponse.Refresh_Token, tokenResponse.Expires_In);

            Log.Information("Xero token refreshed successfully.");
        }

        private void SetSessionToken(string accessToken, string refreshToken, int expiresIn)
        {
            HttpContext.Session.SetString("AccessToken", accessToken);
            HttpContext.Session.SetString("RefreshToken", refreshToken);
            HttpContext.Session.SetString("ExpiresAt", DateTime.UtcNow.AddSeconds(expiresIn).ToString("o"));
        }

        private bool IsAccessTokenValid()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("AccessToken")) &&
                   !string.IsNullOrEmpty(HttpContext.Session.GetString("RefreshToken")) &&
                   !string.IsNullOrEmpty(HttpContext.Session.GetString("ExpiresAt"));
        }

        private bool IsTokenExpired()
        {
            var expiresAtStr = HttpContext.Session.GetString("ExpiresAt");
            if (string.IsNullOrEmpty(expiresAtStr)) return true;

            return DateTime.UtcNow >= DateTime.Parse(expiresAtStr);
        }
    }

    public class XeroConfiguration
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string CallbackUri { get; set; }
    }

    public class XeroTokenResponse
    {
        public string Access_Token { get; set; }
        public string Refresh_Token { get; set; }
        public int Expires_In { get; set; } // Expiry time in seconds
        public string Token_Type { get; set; }
    }
}
