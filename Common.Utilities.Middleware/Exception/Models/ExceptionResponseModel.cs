/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

namespace Common.Utilities.Middleware.Exceptions.Models
{
		public class ExceptionResponseModel
		{
				public bool IsError { get; set; }
				public string ErrorType { get; set; }
				public string ErrorMessage { get; set; }

		}
}
