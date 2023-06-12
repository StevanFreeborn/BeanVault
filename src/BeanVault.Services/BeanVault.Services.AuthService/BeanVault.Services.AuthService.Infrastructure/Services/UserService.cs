namespace BeanVault.Services.AuthService.Infrastructure.Services;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;
  private readonly UserManager<ApplicationUser> _userManager;
  public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
  {
    _userRepository = userRepository;
    _userManager = userManager;
  }

  public async Task<ApplicationUser> AddUserAsync(ApplicationUser user)
  {
    if (user.Email == null)
    {
      throw new ApplicationException("No email provided for user");
    }

    var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);

    if (existingUser != null)
    {
      throw new ApplicationException($"User already exists with email: {user.Email}");
    }

    var result = await _userRepository.AddUserAsync(user);

    if (result is null)
    {
      throw new ApplicationException("Unable to add new user");
    }

    if (result.Succeeded is false)
    {
      throw new AggregateException(
        result.Errors.Select(e => new ApplicationException(e.Description)).ToList()
      );
    }

    var createdUser = await _userRepository.GetUserByEmailAsync(user.Email!);

    if (createdUser is null)
    {
      throw new ApplicationException("Unable to find created user");
    }

    return createdUser;
  }

  public async Task<ApplicationUser> LogInUserAsync(ApplicationUser user)
  {
    if (user.UserName == null)
    {
      throw new ApplicationException("No username provided for user");
    }

    if (user.Password == null)
    {
      throw new ApplicationException("No password provided for user");
    }

    var userLoggingIn = await _userRepository.GetUserByUsernameAsync(user.UserName);

    if (userLoggingIn is null)
    {
      throw new ApplicationException($"No user found with username: {user.UserName}");
    }

    var isCorrectPassword = await _userManager.CheckPasswordAsync(userLoggingIn, user.Password);

    if (isCorrectPassword is false)
    {
      throw new ApplicationException("Invalid password");
    }

    return userLoggingIn;
  }

  public async Task<ApplicationUser?> GetUserByIdAsync(string id)
  {
    return await _userRepository.GetUserByIdAsync(id);
  }
}