using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ManageCollections.Application.Interfaces;
using ManageCollections.Application.Models.Token;
using ManageCollections.Application.Models.UserCredentials;
using ManageCollections.Domain.Entities;
using ManageCollections.Domain.Entities.IdentityEntities;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public JwtService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public Task<TokenResponseModel> CreateTokenAsync(UserLogin user)
        {
            throw new NotImplementedException();
        }

        /*    public async Task<TokenResponseModel> CreateTokenAsync(UserLogin user)
            {
                User foundUser = await NewMethod(user);

                if(foundUser == null) return new TokenResponseModel();

                List<Claim> permissions = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, user.Email)
                };
                foreach (User role in foundUser.Roles)
                {
                    foreach (Permission permission in role.)
                    {

                    }
                }

                throw new NotImplementedException();
            }*/

        public Task<string> GenerateRefreshTokenAsync(UserLogin user)
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            throw new NotImplementedException();
        }

     /*   private async Task<User> NewMethod(UserLogin user)
        {
            return await _userService.GetAsync(x => x.Email == user.Email);
        }*/
    }
}
