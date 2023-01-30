using LinqKit;
using UserManagementCommons.Helpers;
using UserManagementCommons.Interfaces.Repositories;
using UserManagementCommons.Models;

namespace UserManagementCommons.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public User GetUser(string username) => _context.Users.FirstOrDefault(user => user.Username.ToLower() == username.ToLower());

        public IEnumerable<User> GetFirst10Users() => _context.Users.OrderBy(u => u.FirstName)
                                                                    .ThenBy(u => u.LastName)
                                                                    .ThenBy(u => u.Username)
                                                                    .Take(10);
        
        public IEnumerable<User> GetNext10Users(int position) => _context.Users.OrderBy(u => u.FirstName)
                                                                               .ThenBy(u => u.LastName)
                                                                               .ThenBy(u => u.Username)
                                                                               .Skip(position)
                                                                               .Take(10);
        
        public IEnumerable<User> GetNext10UsersWithCriteria(SearchCriteria criteria) => BasicSearch(criteria.SearchValue.Split(" "))
                                                                                       .Skip(criteria.NextPosition)
                                                                                       .Take(10);

        public IEnumerable<User> SearchUsers(string userSearch) => BasicSearch(userSearch.Split(" ")).Take(10);

        public User RegisterUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        private IOrderedQueryable<User> BasicSearch(params string[] userSearch) => _context.Users.AsExpandable()
                                                                                                 .Where(SearchUsersCondition(userSearch))
                                                                                                 .OrderBy(u => u.FirstName)
                                                                                                 .ThenBy(u => u.LastName)
                                                                                                 .ThenBy(u => u.Username);

        private ExpressionStarter<User> SearchUsersCondition(string[] userSearch)
        {
            var predicate = PredicateBuilder.New<User>();

            foreach (string keyword in userSearch)
            {
                predicate = predicate.Or(p => p.Username.ToLower().Contains(keyword.ToLower()))
                                     .Or(p => p.FirstName.ToLower().Contains(keyword.ToLower()))
                                     .Or(p => p.LastName.ToLower().Contains(keyword.ToLower()));

            }

            return predicate;
        }
    }
}
