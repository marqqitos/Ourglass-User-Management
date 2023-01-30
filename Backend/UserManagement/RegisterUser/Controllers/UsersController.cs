using Microsoft.AspNetCore.Mvc;
using UserManagementCommons.Exceptions;
using UserManagementCommons.Interfaces.Services;
using UserManagementCommons.Models;

namespace SearchUsers.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public ActionResult<User> RegisterUser([FromBody] User user)
        {
            try
            {
                var insertedUser = _userService.RegisterUser(user);

                return Ok(insertedUser);
            }
            catch (InvalidUsernameException ex)
            {
                return BadRequest(new ErrorMessage { Message = ex.Message });
            }
        }

    }
}
