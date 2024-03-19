using Newtonsoft.Json;
using Portfolio.API.Utils;
using Portfolio.Core.ExceptionHandles;
using Portfolio.Core.Helpers;

namespace Portfolio.API.Middlewares;

public class ExceptionHandlerMiddleware
{
	private readonly RequestDelegate _next;
	public static List<Exception> Exceptions { get; set; }

	public ExceptionHandlerMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			await HandleExceptionAsync(context, ex);
		}
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		context.Response.ContentType = "application/json";
		context.Response.StatusCode = StatusCodes.Status500InternalServerError;
		ViewModels.ResultViewModel response;
		string JSONResult;

		// Verify is a custom exception
		if (exception.InnerException is NotFoundEntityException)
		{
			context.Response.StatusCode = StatusCodes.Status404NotFound;
			response = Responses.NotFoundErrorMessage(exception.Message);
		}
		else if (exception is ServiceException)
		{
			response = EnvironmentHelper.IsDevelopmentMode
				? Responses.InternalServerErrorMessage(exception.Message, "Services")
				: Responses.InternalServerErrorMessage();
		}
		else
		{
			response = EnvironmentHelper.IsDevelopmentMode
				? Responses.InternalServerErrorMessage(exception.Message)
				: Responses.ApplicationErrorMessage();
		}

		// Log the exception
		Console.WriteLine($"{exception.Message}\n{exception.StackTrace}");

		// Send the response
		JSONResult = JsonConvert.SerializeObject(response);
		return context.Response.WriteAsync(JSONResult);
	}
}
