using BibliotecaDLL.DbContexts;
using MongoDB.Driver;
using nrmcontrolextension.Filters;
using nrmcontrolextension.IRepositories;
using nrmcontrolextension.Models;
using nrmcontrolextension.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nrmcontrolextension.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDbContext _Connection;
        public UserRepository()
        {
            this._Connection = new MongoDbContext();
        }

        public async Task<List<User>> GetUsers(UserFilter userFilter)
        {
            FilterDefinitionBuilder<User> builder = Builders<User>.Filter;
            FilterDefinition<User> filter = builder.Empty;
            if (userFilter != null && !string.IsNullOrEmpty(userFilter.UserId))
            {
                filter &= builder.Where(u => u.UserId == userFilter.UserId);
            }
            List<User> listaUsuarios = await _Connection.Users.Find(filter).ToListAsync();
            return listaUsuarios;
        }

        public async Task<User?> GetUser(User user)
        {
            UserFilter userFilter = new() { UserId = user.UserId };
            User? userBase = (await GetUsers(userFilter)).FirstOrDefault();

            return userBase;
        }

        public async Task<User> CreateUser(User user)
        {
            ValidateUser(user);

            FilterDefinition<User> filter = Builders<User>.Filter.Where(u => u.UserId == user.UserId);
            User userBase = await _Connection.Users.Find(filter).FirstOrDefaultAsync();
            if (userBase == null)
            {
                user.Password = Password.CryptPassword(user.Password);
                await _Connection.Users.InsertOneAsync(user);
            }
            else
            {
                throw new ArgumentException("Usuário já existe!");
            }
            user.Exists = true;
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            ValidateUser(user);
            FilterDefinition<User> filter = Builders<User>.Filter.Where(u => u.UserId == user.UserId);
            UpdateDefinitionBuilder<User> updateBuilder = Builders<User>.Update;
            UpdateDefinition<User> update = updateBuilder.Set(u => u.UserId, user.UserId);
            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = Password.CryptPassword(user.Password);
                update = update.Set(u => u.Password, user.Password);
            }
            await _Connection.Users.UpdateOneAsync(filter, update);
            user.Exists = true;
            return user;
        }

        public async Task DeleteUser(User user)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Where(u => u.UserId == user.UserId);
            await _Connection.Users.DeleteOneAsync(filter);
        }

        public static void ValidateUser(User user)
        {
            if (string.IsNullOrEmpty(user.UserId))
            {
                throw new ArgumentNullException(nameof(user));
            }
            else if (string.IsNullOrEmpty(user.Password) && !user.Exists)
            {
                throw new ArgumentNullException(nameof(user));

            }
        }
    }
}
