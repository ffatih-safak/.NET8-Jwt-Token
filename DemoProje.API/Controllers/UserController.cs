using DemoProje.Core.Contracts;
using DemoProje.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoProje.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginDTO loginDTO)
        {
            var result = await _user.LoginUserAsync(loginDTO);
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegisterUserDTO registerUserDTO)
        {
            var result = await _user.RegiserUserAsync(registerUserDTO);
            return Ok(result);
        }
    }
}
