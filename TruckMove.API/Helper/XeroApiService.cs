using Xero.NetStandard.OAuth2.Client;
using Xero.NetStandard.OAuth2.Config;
using Xero.NetStandard.OAuth2.Token;

namespace TruckMove.API.Helper
{
    public class XeroApiService
    {

        private readonly XeroClient _xeroClient;

        public XeroApiService(XeroConfiguration xeroConfig)
        { 
            _xeroClient = new XeroClient(xeroConfig);
        }
        public async Task AuthenticateAsync(string authorizationCode)
        {
            var xeroToken = await _xeroClient.RequestAccessTokenAsync(authorizationCode);

            // Save the token for future API calls
            SaveToken(xeroToken);
        }

        private void SaveToken(IXeroToken xeroToken)
        {
            // Implement your token storage logic here
            // Ensure tokens are stored securely
        }
        // Use _xeroConfig in your methods
    }
}
