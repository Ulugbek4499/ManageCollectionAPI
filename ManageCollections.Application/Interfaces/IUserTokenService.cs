using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ManageCollections.Application.Models.UserCredentials;
using ManageCollections.Domain.Entities;

namespace ManageCollections.Application.Interfaces
{
    public interface IUserTokenService
    {
        Task<bool> AuthenAsync(UserLogin user);
        Task<UserRefreshToken> AddUsersRefreshTokens(UserRefreshToken user);
        Task<UserRefreshToken> UpdateUserRefreshTokens(UserRefreshToken user);
        Task<UserRefreshToken> GetSavedRefreshTokens(string email, string refreshtoken);
        Task<bool> DeleteUserRefreshTokens(string email, string refreshToken);
    }
}
