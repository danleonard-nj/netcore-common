/* Copyright (C) 2021 Dan Leonard
 * General public license applies */

using Microsoft.AspNetCore.Builder;

namespace Common.Utilities.Swagger
{
		public static class SwaggerExtensions
		{
				public static void ConfigureSwagger(this IApplicationBuilder app, string applicationName)
				{
						app.UseSwagger();

						app.UseSwaggerUI(c =>
						{
								c.SwaggerEndpoint("/swagger/v1/swagger.json", applicationName);
								c.RoutePrefix = string.Empty;
						});
				}
		}
}
