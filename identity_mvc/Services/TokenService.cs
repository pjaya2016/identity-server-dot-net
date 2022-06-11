using IdentityModel.Client;

namespace identity_mvc.Services
{
    public class TokenService : ITokenService
    {
        private readonly DiscoveryDocumentResponse _discoveryDocumentResponse;

        public TokenService() { 
        
            using var httpClient = new HttpClient();
            _discoveryDocumentResponse = httpClient.GetDiscoveryDocumentAsync("https://localhost:5443").Result;

            if (_discoveryDocumentResponse.IsError) {
                throw new Exception("Unable to connect");
            }

        }

        public async Task<TokenResponse> getToken(string scope)
        {
           using var httpClient = new HttpClient();
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {

                Address = _discoveryDocumentResponse.TokenEndpoint,
                ClientId = "m2m.client",
                ClientSecret = "SuperSecretPassword",
                Scope = scope

            }) ;

            if (tokenResponse.IsError) {
                throw new Exception("Unable to connect");
            }
            return tokenResponse;
        }
    }
}
