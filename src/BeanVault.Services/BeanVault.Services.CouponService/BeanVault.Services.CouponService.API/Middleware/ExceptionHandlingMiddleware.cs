namespace BeanVault.Services.CouponService.API.Middleware;

public class ExceptionHandlingMiddleware
{
  private readonly RequestDelegate _next;

  public ExceptionHandlingMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (Exception ex)
    {
      var problem = GetProblemDetails(ex);
      context.Response.StatusCode = problem.Status!.Value;
      context.Response.ContentType = "application/problem+json";
      await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
    }
  }

  private static ProblemDetails GetProblemDetails(Exception ex)
  {
    var code = ex switch
    {
      ModelNotFoundException => HttpStatusCode.NotFound,
      _ => HttpStatusCode.InternalServerError,
    };

    var problemDetails = new ProblemDetails
    {
      Status = (int) code,
      Title = "A problem occurred while processing the request",
      Detail = ex.Message,
    };

    if (ex is AggregateException aggregateException)
    {
      problemDetails.Extensions.Add(
        "errors",
        aggregateException.InnerExceptions.Select(ex => ex.Message).ToList()
      );
    }

    return problemDetails;
  }
}