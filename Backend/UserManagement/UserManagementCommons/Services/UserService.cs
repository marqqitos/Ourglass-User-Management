using UserManagementCommons.Exceptions;
using UserManagementCommons.Helpers;
using UserManagementCommons.Interfaces.Repositories;
using UserManagementCommons.Interfaces.Services;
using UserManagementCommons.Models;

namespace UserManagementCommons.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUser(string username) => _userRepository.GetUser(username);

        public IEnumerable<User> GetFirst10Users() => _userRepository.GetFirst10Users();
        public IEnumerable<User> GetNext10Users(SearchCriteria criteria)
        {
            if(criteria.SearchValue is null or "")
            {
                return _userRepository.GetNext10Users(criteria.NextPosition);
            }

            return _userRepository.GetNext10UsersWithCriteria(criteria);
        }

        public IEnumerable<User> SearchUsers(string userSearch) => _userRepository.SearchUsers(userSearch);

        public User RegisterUser(User user)
        {
            var existingUser = GetUser(user.Username);

            if (existingUser != null)
            {
                throw new InvalidUsernameException();
            }

            var insertedUser = _userRepository.RegisterUser(user);

            return insertedUser;
        }
    }
}
