using igrejaextensions.Filters;
using igrejaextensions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace igrejaextensions.IServices
{
    public interface IUserService
    {
        public Task<List<User>> GetUsers(UserFilter userFilter);
        public Task<User> CreateUser(User user);
        public Task<User> UpdateUser(User user);
        public Task<bool> ValidateLogin(User user);
        public Task DeleteUser(User user);
    }
}
