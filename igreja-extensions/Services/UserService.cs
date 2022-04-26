using igrejaextensions.Filters;
using igrejaextensions.IRepositories;
using igrejaextensions.IServices;
using igrejaextensions.Models;
using igrejaextensions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace igrejaextensions.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _IUserRepository;
        public UserService(IUserRepository iUserRepository)
        {
            this._IUserRepository = iUserRepository;
        }
        public async Task<List<User>> GetUsers(UserFilter userFilter)
        {
            return await this._IUserRepository.GetUsers(userFilter);
        }

        public async Task<User> CreateUser(User user)
        {
            return await this._IUserRepository.CreateUser(user);
        }

        public async Task<User> UpdateUser(User user)
        {
            return await this._IUserRepository.UpdateUser(user);
        }

        public Task DeleteUser(User user)
        {
            return this._IUserRepository.DeleteUser(user);
        }

        public async Task<bool> ValidateLogin(User user)
        {
            User? userBase = await this._IUserRepository.GetUser(user);
            if (userBase == null)
            {
                throw new UnauthorizedAccessException("Usuário não encontrado!");
            }
            else if (Password.CryptPassword(user.Password) != userBase.Password)
            {
                throw new UnauthorizedAccessException("Usuário não encontrado!");
            }
            return true;
        }
    }
}
