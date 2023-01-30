using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementCommons.Models;

namespace UserManagementCommons.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public User GetUser(string username);
        public User RegisterUser(User user);
        public IEnumerable<User> GetFirst10Users();
        public IEnumerable<User> GetNext10Users(int position);
        public IEnumerable<User> GetNext10UsersWithCriteria(SearchCriteria criteria);
        public IEnumerable<User> SearchUsers(string userSearch);
    }
}
