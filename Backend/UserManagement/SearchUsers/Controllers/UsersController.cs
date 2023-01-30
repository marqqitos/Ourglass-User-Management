using Microsoft.AspNetCore.Mvc;
using UserManagementCommons.Interfaces.Services;
using UserManagementCommons.Models;

namespace SearchUsers.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("users/search")]
        public ActionResult<IEnumerable<User>> SearchUsers([FromQuery] string searchCriteria)
        {
            var users = _userService.SearchUsers(searchCriteria);

            return Ok(users);
        }

        [HttpGet]
        [Route("users")]
        public ActionResult<IEnumerable<User>> GetFirst10Users()
        {
            var users = _userService.GetFirst10Users();

            return Ok(users);
        }

        [HttpPost]
        [Route("users/next")]
        public ActionResult<IEnumerable<User>> GetNext10Users([FromBody] SearchCriteria searchCriteria)
        {
            var users = _userService.GetNext10Users(searchCriteria);

            return Ok(users);
        }
    }
}
