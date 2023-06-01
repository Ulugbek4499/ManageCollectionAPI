using System.Security.Cryptography;
using System.Text;
using ManageCollections.Application.Abstractions;
using ManageCollections.Application.Interfaces;
using ManageCollections.Application.Models.Token;
using ManageCollections.Domain.Entities;

namespace Infrastructure.Services
{
    public delegate Task DeleteRefreshTokenDelegate(UserRefreshToken configuration);

    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IManageCollectionDbContext _db;

        private DeleteRefreshTokenDelegate deleteRefresh;

        public UserRepository(IManageCollectionDbContext db) : base(db)
        {
            _db = db;
            deleteRefresh = DeleteRefreshToken;
        }

        public UserRefreshToken AddUserRefreshTokens(UserRefreshToken user)
        {
            _db.UserRefreshTokens.Add(user);
            deleteRefresh.Invoke(user);
            return user;
        }

        public UserRefreshToken? GetSavedRefreshTokens(string refreshToken)
        {
            return _db.UserRefreshTokens.FirstOrDefault(x => x.RefreshToken == refreshToken);

        }

        public async Task<int> SaveCommit()
        {
            return await _db.SaveChangesAsync();
        }

        public async Task<UserRefreshToken> UpdateUserRefreshTokens(UserRefreshToken userRefreshTokens)
        {
            var user = _db.UserRefreshTokens.FirstOrDefault(x => x.UserName == userRefreshTokens.UserName);
            if (user is not null)
                _db.UserRefreshTokens.Remove(user);
            await _db.SaveChangesAsync();
            await _db.UserRefreshTokens.AddAsync(userRefreshTokens);
            await _db.SaveChangesAsync();
            return userRefreshTokens;
        }

        public List<UserRefreshToken> GetAllUserRefreshTokens()
        {
            return _db.UserRefreshTokens.ToList();
        }

        public UserRefreshToken? GetUserRefreshTokensById(Guid id)
        {
            return _db.UserRefreshTokens.FirstOrDefault(x => x.Id == id);
        }

        public async Task DeleteRefreshToken(UserRefreshToken user)
        {
            await Task.Delay(TimeSpan.FromMinutes(3));
            _db.UserRefreshTokens.Remove(user);
            await _db.SaveChangesAsync();
        }

        public override async Task<User?> CreateAsync(User entity)
        {
            if (_collectionDb.Users.FirstOrDefault(x => x.UserName == entity.UserName) is not null || entity.UserName == null)
            {
                return null;
            }


            if (entity.Password is not null)
                entity.Password = ComputeHash(entity.Password);
            await _collectionDb.Users.AddAsync(entity);
            await _collectionDb.SaveChangesAsync();
            return entity;
        }

        public string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
