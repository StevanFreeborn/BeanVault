namespace BeanVault.Services.AuthService.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/users")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
  private readonly IUserRepository _userRepository;

  public UsersController(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  [MapToApiVersion("1.0")]
  [HttpGet("{id}")]
  public async Task<IActionResult> GetUserById(string id)
  {
    var user = await _userRepository.GetUserByIdAsync(id);

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
    // TODO: Need to verify user doesn't already exist with given email
    // before creating user.
    var user = addUserDto.ToApplicationUser();
    var createdUser = await _userRepository.AddUserAsync(user);
    return CreatedAtAction(
      nameof(GetUserById),
      new { id = createdUser.Id },
      new UserDto(createdUser)
    );
  }

  [MapToApiVersion("1.0")]
  [HttpPost("login")]
  public IActionResult Login()
  {
    return Ok();
  }
}