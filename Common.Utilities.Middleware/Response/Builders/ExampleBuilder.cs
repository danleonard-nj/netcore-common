/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

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
