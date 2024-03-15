using Microsoft.AspNetCore.Mvc;
using Portfolio.API.ViewModels;

namespace Portfolio.API.Utils;

public class Responses
{
	public static ResultViewModel ApplicationErrorMessage()
	{
		return new ResultViewModel
		{
			Message = "An error occurred in the application, please try again.",
			Success = false,
		};
	}

	public static ResultViewModel SuccessMessage(string message)
	{
		return new ResultViewModel { Message = message, Success = true };
	}

	public static ResultViewModel SuccessLoginMessage(ResultLoginViewModel result)
	{
		return new ResultViewModel
		{
			Message = "User logged in successfully!",
			Success = true,
			Data = result
		};
	}

	public static ResultViewModel NotFoundErrorMessage()
	{
		return new ResultViewModel { Message = "Registry is not found", Success = false };
	}

	public static ResultViewModel NotFoundErrorMessage(string message)
	{
		return new ResultViewModel { Message = message, Success = false };
	}

	public static ResultViewModel DomainErrorMessage(string message)
	{
		return new ResultViewModel { Message = message, Success = false };
	}

	public static ResultViewModel ServiceUnavailableErrorMessage()
	{
		return new ResultViewModel
		{
			Message = "The service is currently unavailable, please try again later.",
			Success = false,
		};
	}

	public static ResultViewModel ServiceUnavailableErrorMessage(string message)
	{
		return new ResultViewModel { Message = message, Success = false, };
	}

	public static ResultViewModel UnauthorizedErrorMessage()
	{
		return new ResultViewModel { Message = "The login and password combination is incorrect!", Success = false, };
	}

	public static ResultViewModel InternalServerErrorMessage()
	{
		return new ResultViewModel
		{
			Message = "An internal error occurred in the application, please try again.",
			Success = false,
		};
	}

	public static ResultViewModel InternalServerErrorMessage(string message)
	{
		return new ResultViewModel { Message = message, Success = false, };
	}

	public static ResultViewModel InternalServerErrorMessage(string message, string scopeError)
	{
		return new ResultViewModel { Message = $"{scopeError}:{message}", Success = false, };
	}

	public static ResultViewModel S3Response(string message, bool success)
	{
		return new ResultViewModel { Message = message, Success = success, };
	}
}
