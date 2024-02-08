using Microsoft.AspNetCore.Mvc;
using StudentCRUD.DTOs.UserAccount;
using StudentCRUD.Services.UserAccount;

namespace StudentCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AddUserRegisterDto addUserRegisterDto)
        {
            var response = await _userService.CreateAccountAsync(addUserRegisterDto);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var response = await _userService.LoginAccountAsync(userLoginDto);
            return Ok(response);
        }
    }
}
