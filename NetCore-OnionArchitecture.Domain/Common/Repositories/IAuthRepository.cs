

using NetCore_OnionArchitecture.Domain.Entities;

namespace NetCore_OnionArchitecture.Domain.Common.Repositories
{
    public interface IAuthRepository
    {
        string CreateToken(User user);
        Task<User> Login(string userName, string password);
        Task<User> Register(User user);
        Task<User> GetUserByNameAsync(string userName);
    }
}
