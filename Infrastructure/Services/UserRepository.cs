using System.Security.Cryptography;
using System.Text;
using ManageCollections.Application.Abstractions;
using ManageCollections.Application.Interfaces;
using ManageCollections.Domain.Entities;

namespace Infrastructure.Services
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IManageCollectionDbContext collectionDbContext) : base(collectionDbContext)
        {
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
            using SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            StringBuilder builder = new();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
