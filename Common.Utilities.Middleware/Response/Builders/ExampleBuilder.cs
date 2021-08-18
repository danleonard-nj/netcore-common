/* Copyright (C) 2021 Dan Leonard
 * 
 * This is free software: you can redistribute it and/or modify it under 
 * the terms of the GNU General Public License as published by the Free 
 * Software Foundation, either version 3 of the License, or (at your option) 
 * any later version.
 * 
 * This software is distributed in the hope that it will be useful, but 
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License 
 * for more details.
 */


using Common.Utilities.Middleware.Response.Abstractions;
using Newtonsoft.Json;
using System;

namespace Common.Utilities.Middleware.Response.Builders
{
		public class ExampleBuilder : IResponseBuilder
		{
				public object CreateResponse(string body)
				{
						var response = new ResponseContentModel
						{
								Content = JsonConvert.DeserializeObject(body)
						};

						return response;
				}

				public class ResponseContentModel
				{
						public string RequestId { get; set; } = Guid.NewGuid().ToString();
						public DateTime RequestDate { get; set; } = DateTime.Now;
						public object Content { get; set; }
				}
		}
}
