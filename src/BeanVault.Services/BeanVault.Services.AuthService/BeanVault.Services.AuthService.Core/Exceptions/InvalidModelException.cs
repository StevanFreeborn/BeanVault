namespace BeanVault.Services.AuthService.Core.Exceptions;

public class InvalidModelException : Exception
{
  public InvalidModelException(string message) : base(message)
  {
  }

  public InvalidModelException(string message, Exception innerException) : base(message, innerException)
  {
  }
}