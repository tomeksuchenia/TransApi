using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trans.Infrastructure.Commands.Users;
using Trans.Infrastructure.Dto;
using Trans.Infrastructure.Services;

namespace Trans.Api.Controllers
{
    [Route("users/account")]
    public class AccountController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenService _jwtToken;
        private readonly IAccountService _accountService;

        public AccountController(IUserService userService, IJwtTokenService jwtToken,
            IAccountService accountService)
        {
            _userService = userService;
            _jwtToken = jwtToken;
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<JwtDto>> Post([FromBody]Login command)
        {
            await _userService.LoginAsync(command.Email, command.Password);
            var user = await _userService.GetAsync(command.Email);
            var jwtToken = _jwtToken.Create(user.Id, user.Role);

            return jwtToken;
        }

        [Authorize]
        [HttpPut("password")]
        public async Task<ActionResult> Put([FromBody] ChangeUserPassword command)
        {
            await _accountService.ChangePassword(UserId, command.oldPassword, command.newPassword, command.repeatNewPassword);
            return Ok();
        }

        [Authorize]
        [HttpDelete("me")]
        public async Task<ActionResult> Delete()
        {
            await _userService.DeleteAsync(UserId);
            return NoContent();
        }
    }
}
