using IdentityModel.Client;

namespace identity_mvc.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> getToken(string scope);
    }
}
