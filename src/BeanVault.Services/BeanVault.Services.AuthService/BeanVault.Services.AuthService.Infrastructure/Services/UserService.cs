namespace BeanVault.Services.AuthService.Infrastructure.Services;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly RoleManager<IdentityRole> _roleManager;
  public UserService(
    IUserRepository userRepository,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager
  )
  {
    _userRepository = userRepository;
    _userManager = userManager;
    _roleManager = roleManager;
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

  public async Task AddRoleToUserAsync(string userId, string roleName)
  {
    var user = await _userRepository.GetUserByIdAsync(userId);

    if (user is null)
    {
      throw new ApplicationException($"Unable to find user with id: {userId}");
    }

    var doesRoleExist = await _roleManager.RoleExistsAsync(roleName);

    if (doesRoleExist == false)
    {
      var roleCreationResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

      if (roleCreationResult.Succeeded == false)
      {
        throw new AggregateException(
          roleCreationResult.Errors.Select(e => new ApplicationException(e.Description)).ToList()
        );
      }
    }

    var roleAssignmentResult = await _userManager.AddToRoleAsync(user, roleName);

    if (roleAssignmentResult.Succeeded == false)
    {
      throw new AggregateException(
        roleAssignmentResult.Errors.Select(e => new ApplicationException(e.Description)).ToList()
      );
    }
  }
}