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

  /// <summary>
  /// Get user by id
  /// </summary>
  [Authorize]
  [MapToApiVersion("1.0")]
  [HttpGet("{id}")]
  [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetUserById(string id)
  {
    var user = await _userService.GetUserByIdAsync(id);
    var userDto = new UserDto(user);
    return Ok(userDto);
  }

  /// <summary>
  /// Register a new user
  /// </summary>
  [MapToApiVersion("1.0")]
  [HttpPost("register")]
  [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
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

  /// <summary>
  /// Log a user in
  /// </summary>
  [MapToApiVersion("1.0")]
  [HttpPost("login")]
  [ProducesResponseType(typeof(AuthUserDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Login(LoginUserDto loginUserDto)
  {
    var user = loginUserDto.ToApplicationUser();
    var loggedInUser = await _userService.LogInUserAsync(user);
    var userRoles = await _userService.GetUserRolesAsync(loggedInUser);
    var (expiration, token) = _jwtTokenService.GenerateToken(loggedInUser, userRoles);
    var authUserDto = new AuthUserDto(loggedInUser, token, expiration, userRoles);
    return Ok(authUserDto);
  }

  /// <summary>
  /// Add a role to a user
  /// </summary>
  [Authorize(Roles = "admin")]
  [MapToApiVersion("1.0")]
  [HttpPost("add-role")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> AddRole(AddRoleToUserDto addRoleToUserDto)
  {
    await _userService.AddRoleToUserAsync(addRoleToUserDto.UserId, addRoleToUserDto.RoleName);
    return Ok();
  }
}