using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ManageCollections.Application.Models.Token;
using ManageCollections.Application.Models.UserCredentials;

namespace ManageCollections.Application.Interfaces
{
    public interface IJwtService
    {
        public Task<TokenResponseModel> CreateTokenAsync(UserLogin user);
        Task<string> GenerateRefreshTokenAsync(UserLogin user);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
