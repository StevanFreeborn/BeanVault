namespace BeanVault.Services.AuthService.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/users")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
  private readonly IUserService _userService;
  private readonly IJwtTokenService _jwtTokenService;

  public UsersController(IUserService userService, IJwtTokenService jwtTokenService)
  {
    _userService = userService;
    _jwtTokenService = jwtTokenService;
  }

  [MapToApiVersion("1.0")]
  [HttpGet("{id}")]
  public async Task<IActionResult> GetUserById(string id)
  {
    var user = await _userService.GetUserByIdAsync(id);

    if (user == null)
    {
      return NotFound();
    }

    var userDto = new UserDto(user);
    return Ok(userDto);
  }

  [MapToApiVersion("1.0")]
  [HttpPost("register")]
  public async Task<IActionResult> Register(AddUserDto addUserDto)
  {
    var user = addUserDto.ToApplicationUser();
    var createdUser = await _userService.AddUserAsync(user);
    return CreatedAtAction(
      nameof(GetUserById),
      new { id = createdUser.Id },
      new UserDto(createdUser)
    );
  }

  [MapToApiVersion("1.0")]
  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginUserDto loginUserDto)
  {
    var user = loginUserDto.ToApplicationUser();
    var loggedInUser = await _userService.LogInUserAsync(user);
    var token = _jwtTokenService.GenerateToken(loggedInUser);
    var authUserDto = new AuthUserDto(loggedInUser, token);
    return Ok(authUserDto);
  }

  [MapToApiVersion("1.0")]
  [HttpPost("add-role")]
  public async Task<IActionResult> AddRole(AddRoleToUserDto addRoleToUserDto)
  {
    await _userService.AddRoleToUserAsync(addRoleToUserDto.UserId, addRoleToUserDto.RoleName);
    return Ok();
  }
}