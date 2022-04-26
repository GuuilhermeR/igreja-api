using nrmcontrolextension.Filters;
using nrmcontrolextension.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nrmcontrolextension.IRepositories
{
    public interface IUserRepository
    {

        Task<List<User>> GetUsers(UserFilter userFilter);

        Task<User> CreateUser(User user);

        Task<User> UpdateUser(User user);
        Task<User?> GetUser(User user);
        Task DeleteUser(User user);
    }
}
