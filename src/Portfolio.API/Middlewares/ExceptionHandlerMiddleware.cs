using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Portfolio.API.Utils;
using Portfolio.Core.ExceptionHandles;

namespace Portfolio.API.Middlewares;

public class ExceptionHandlerMiddleware
{
	private readonly RequestDelegate _next;

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

	private Task HandleExceptionAsync(HttpContext context, Exception ex)
	{
		bool isDevelopmentMode = Environment.GetEnvironmentVariable("SERVER_MODE") == "development";

		ViewModels.ResultViewModel response = isDevelopmentMode
			? Responses.InternalServerErrorMessage()
			: Responses.InternalServerErrorMessage(ex.Message);

		if (isDevelopmentMode)
		{
			Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
		}

		context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

		var result = JsonConvert.SerializeObject(response);
		context.Response.StatusCode = StatusCodes.Status500InternalServerError;
		context.Response.ContentType = "application/json";
		return context.Response.WriteAsync(result);
	}
}
