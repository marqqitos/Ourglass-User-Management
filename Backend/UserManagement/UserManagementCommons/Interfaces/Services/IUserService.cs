using UserManagementCommons.Models;

namespace UserManagementCommons.Interfaces.Services
{
    public interface IUserService
    {
        public User GetUser(string username);
        public User RegisterUser(User user);
        public IEnumerable<User> GetFirst10Users();
        public IEnumerable<User> GetNext10Users(SearchCriteria criteria);
        public IEnumerable<User> SearchUsers(string userSearch);
    }
}
