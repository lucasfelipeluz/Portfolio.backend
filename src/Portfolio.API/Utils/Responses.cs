using Microsoft.AspNetCore.Mvc;
using Portfolio.API.ViewModels;

namespace Portfolio.API.Utils;

public class Responses
{
	public static ResultViewModel ApplicationErrorMessage()
	{
		return new ResultViewModel
		{
			Message = "Ocorreu um erro na aplicação, por favor tente novamente.",
			Success = false,
		};
	}

	public static ResultViewModel SuccessMessage(string message)
	{
		return new ResultViewModel { Message = message, Success = true };
	}

	public static ResultViewModel NotFoundErrorMessage()
	{
		return new ResultViewModel { Message = "Registro não encontrado!", Success = false };
	}

	public static ResultViewModel NotFoundErrorMessage(string message)
	{
		return new ResultViewModel { Message = message, Success = false };
	}

	public static ResultViewModel DomainErrorMessage(string message)
	{
		return new ResultViewModel { Message = message, Success = false };
	}

	public static ResultViewModel UnauthorizedErrorMessage()
	{
		return new ResultViewModel { Message = "A combinação de login e senha está incorreta!", Success = false, };
	}

	public static ResultViewModel InternalServerErrorMessage()
	{
		return new ResultViewModel
		{
			Message = "Ocorreu um erro interno na aplicação, por favor tente novamente.",
			Success = false,
		};
	}

	public static ResultViewModel InternalServerErrorMessage(string message)
	{
		return new ResultViewModel { Message = message, Success = false, };
	}

	public static ResultViewModel S3Response(string message, bool success)
	{
		return new ResultViewModel { Message = message, Success = success, };
	}
}
