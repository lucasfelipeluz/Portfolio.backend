using System;

namespace Portfolio.Core.ExceptionHandles;

public class NotFoundEntityException : Exception
{
	public NotFoundEntityException(string message)
		: base(message) { }

	public NotFoundEntityException(string message, Exception innerException)
		: base(message, innerException) { }
}
