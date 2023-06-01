using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ManageCollections.Application.Abstractions;
using ManageCollections.Application.Interfaces;
using ManageCollections.Application.Models.Token;
using ManageCollections.Domain.Entities;
using ManageCollections.Domain.Entities.IdentityEntities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IManageCollectionDbContext _dbContext;
        private readonly int _refreshTokenLifetime;
        private readonly int _accessTokenLifetime;

        public TokenService(IConfiguration configuration, IManageCollectionDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            _refreshTokenLifetime = int.Parse(configuration["JWT:RefreshTokenLifetime"]);
            _accessTokenLifetime = int.Parse(configuration["JWT:AccessTokenLifetime"]);
        }
        
        public async Task<Tokens> CreateTokensAsync(User user)
        {
            List<Claim> claims = new()
            {
            new Claim(ClaimTypes.Name, user.UserName)
            };

            foreach (Role item in user.Roles)
            {
                foreach (Permission item2 in item.Permissions)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item2.PermissionName));

                }
            }

            claims = claims.Distinct().ToList();

            Tokens tokens = CreateToken(claims);

            var SavedRefreshToken = Get(x => x.UserName == user.UserName).FirstOrDefault();
            if (SavedRefreshToken == null)
            {
                var refreshToken = new UserRefreshToken()
                {
                    ExpirationDate = DateTime.UtcNow.AddMinutes(_refreshTokenLifetime),
                    RefreshToken = tokens.RefreshToken,
                    UserName = user.UserName
                };
                await AddRefreshToken(refreshToken);

            }
            else
            {
                SavedRefreshToken.RefreshToken = tokens.RefreshToken;
                SavedRefreshToken.ExpirationDate = DateTime.UtcNow.AddMinutes(_refreshTokenLifetime);
                Update(SavedRefreshToken);
            }

            return tokens;
        }

        public Task<Tokens> CreateTokensFromRefresh(ClaimsPrincipal principal, UserRefreshToken savedRefreshToken)
        {
            Tokens tokens = CreateToken(principal.Claims);

            savedRefreshToken.RefreshToken = tokens.RefreshToken;
            savedRefreshToken.ExpirationDate = DateTime.UtcNow.AddMinutes(_refreshTokenLifetime);

            Update(savedRefreshToken);
            return Task.FromResult(tokens);
        }

        public async Task<bool> AddRefreshToken(UserRefreshToken tokens)
        {
            await _dbContext.UserRefreshTokens.AddAsync(tokens);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public ClaimsPrincipal GetClaimsFromExpiredToken(string token)
        {
            byte[] Key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

            var tokenParams = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = _configuration["JWT:Audience"],
                ValidateIssuer = true,
                ValidateLifetime = false,
                ValidIssuer = _configuration["JWT:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            JwtSecurityTokenHandler tokenHandler = new();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenParams, out SecurityToken securityToken);
            JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null)// ||
                                         //jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }

        public bool Update(UserRefreshToken tokens)
        {
            _dbContext.UserRefreshTokens.Update(tokens);
            _dbContext.SaveChangesAsync();
            return true;
        }

        public IQueryable<UserRefreshToken> Get(Expression<Func<UserRefreshToken, bool>> predicate)
        {
            return _dbContext.UserRefreshTokens.Where(predicate);
        }

        public bool Delete(UserRefreshToken token)
        {
            _dbContext.UserRefreshTokens.Remove(token);
            _dbContext.SaveChangesAsync();
            return true;
        }

        private Tokens CreateToken(IEnumerable<Claim> claims)
        {
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddMinutes(_accessTokenLifetime),
                claims: claims,
                signingCredentials: new SigningCredentials(
                                        new SymmetricSecurityKey(
                                            Encoding.UTF8.GetBytes(_configuration["JWT:Key"])),
                                            SecurityAlgorithms.HmacSha256Signature)
                );
            Tokens tokens = new()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = GenerateRefreshToken()
            };
            return tokens;
        }

        private string GenerateRefreshToken()
        {
            return (DateTime.UtcNow.ToString() + _configuration["JWT:Key"]).ComputeSha256Hash();
        }

        

    }
}
