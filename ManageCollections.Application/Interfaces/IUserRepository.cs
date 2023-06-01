using ManageCollections.Application.Interfaces.Repositories;
using ManageCollections.Application.Models.Token;
using ManageCollections.Domain.Entities;

namespace ManageCollections.Application.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        UserRefreshToken AddUserRefreshTokens(UserRefreshToken user);
        Task<UserRefreshToken> UpdateUserRefreshTokens(UserRefreshToken user);
        UserRefreshToken? GetSavedRefreshTokens(string refreshToken);
        Task<int> SaveCommit();
        List<UserRefreshToken> GetAllUserRefreshTokens();
        UserRefreshToken? GetUserRefreshTokensById(Guid id);
        Task DeleteRefreshToken(UserRefreshToken user);
    }
}
