/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

namespace Common.Models.AspNetCore.Response.Abstractions
{
		public interface IResponseModel
		{
				object Content { get; set; }
				object Exception { get; set; }
				bool IsError { get; set; }
		}
}
