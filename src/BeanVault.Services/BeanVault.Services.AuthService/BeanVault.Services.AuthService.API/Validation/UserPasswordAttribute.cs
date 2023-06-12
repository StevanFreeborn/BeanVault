namespace BeanVault.Services.AuthService.API.Validation;

public class UserPasswordAttribute : ValidationAttribute
{
  public override bool IsValid(object? value)
  {
    var password = value as string;

    int requiredLength = 6;
    bool requireNonAlphanumeric = true;
    bool requireDigit = true;
    bool requireLowercase = true;
    bool requireUppercase = true;

    if (password!.Length < requiredLength)
    {
      ErrorMessage = $"The password must be at least {requiredLength} characters long.";
      return false;
    }

    if (requireNonAlphanumeric && Regex.IsMatch(password, @"\W") == false)
    {
      ErrorMessage = "The password must contain at least one non-alphanumeric character.";
      return false;
    }

    if (requireDigit && Regex.IsMatch(password, @"\d") == false)
    {
      ErrorMessage = "The password must contain at least one digit.";
      return false;
    }

    if (requireLowercase && Regex.IsMatch(password, @"[a-z]") == false)
    {
      ErrorMessage = "The password must contain at least one lowercase letter.";
      return false;
    }

    if (requireUppercase && Regex.IsMatch(password, @"[A-Z]") == false)
    {
      ErrorMessage = "The password must contain at least one uppercase letter.";
      return false;
    }

    return true;
  }
}