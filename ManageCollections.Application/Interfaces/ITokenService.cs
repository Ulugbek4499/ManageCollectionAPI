using System.Linq.Expressions;
using System.Security.Claims;
using ManageCollections.Application.Models.Token;
using ManageCollections.Domain.Entities;

namespace ManageCollections.Application.Interfaces
{
    public interface ITokenService
    {
        public Task<Tokens> CreateTokensAsync(User user);
        public Task<Tokens> CreateTokensFromRefresh(ClaimsPrincipal principal, UserRefreshToken savedRefreshToken);
        public ClaimsPrincipal GetClaimsFromExpiredToken(string token);

        public Task<bool> AddRefreshToken(UserRefreshToken tokens);
        public bool Update(UserRefreshToken tokens);
        public IQueryable<UserRefreshToken> Get(Expression<Func<UserRefreshToken, bool>> predicate);
        public bool Delete(UserRefreshToken token);
    }
}
