using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trans.Infrastructure.Commands;
using Trans.Infrastructure.Commands.Users;
using Trans.Infrastructure.Dto;
using Trans.Infrastructure.Services;

namespace Trans.Api.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUserService userService, IEmailSender emailSender, ICommandDispatcher commandDispatcher, ILogger<UsersController> logger)
        {
            _userService = userService;
            _emailSender = emailSender;
            _commandDispatcher = commandDispatcher;
            _logger = logger;
        }

        [HttpGet("/userId/{userId}")]
        public async Task<ActionResult<UserDto>> Get(Guid userId)
        {
            var user = await _userService.GetAsync(userId);
            return Ok(user);
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<UserDto>> Get(string email)
        {
            var user = await _userService.GetAsync(email);
            return Ok(user);
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get([FromQuery]UserQuery query)
        {
            var users = await _userService.BrowseAsync(query.pageSize, query.pageNumber);
            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Post([FromBody]CreateUser command)
        {
            await _commandDispatcher.DispatchAsync(command);
            _logger.LogInformation($"User with email: {command.Email} has been registered");
            //await _emailSender.SendEmailAsync(command.Email, "Registered TransTs", $"Welcome {command.Username} to TransTs platform."); //Server STMP not set
            return Created($"/users/{command.Email}", null);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            await _userService.DeleteAsync(userId);
            return NoContent();
        }


    }
}

