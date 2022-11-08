using Microsoft.IdentityModel.Tokens;

namespace NZWalks.API.Repositories
{
    public interface IUserRepository
    {
        Task<bool> AuthenticateAsync(string username, string password);
    }
}
