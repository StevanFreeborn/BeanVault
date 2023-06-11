namespace BeanVault.Services.AuthService.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/auth")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
  public AuthController()
  {
  }

  [MapToApiVersion("1.0")]
  [HttpPost("register")]
  public async Task<IActionResult> Register()
  {
    return Ok();
  }

  [MapToApiVersion("1.0")]
  [HttpPost("login")]
  public async Task<IActionResult> Login()
  {
    return Ok();
  }
}