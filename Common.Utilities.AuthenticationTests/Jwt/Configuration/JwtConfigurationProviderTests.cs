using Common.Utilities.UnitTesting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Reflection;

namespace Common.Utilities.Authentication.Jwt.Configuration.Tests
{
		[TestClass()]
		public class JwtConfigurationProviderTests
		{
				private ModelProvider _modelProvider;

				[TestInitialize]
				public void BeforeEach()
				{
						_modelProvider = new ModelProvider();

						//_modelProvider.Register<TokenValidationParameters>(_parameters);
				}

				[TestMethod()]
				public void ConfigureJwtAuthenticationTest()
				{
						// Arrange

						var serviceCollection = new ServiceCollection();

						var configuration = new Mock<IConfiguration>();

						// Act

						var callingAssembly = Assembly.GetCallingAssembly();

						var assembly = Assembly.GetCallingAssembly().GetManifestResourceNames();

						var directory = Environment.CurrentDirectory;

						Assert.IsTrue(true);
				}

				private readonly TokenValidationParameters _parameters = new TokenValidationParameters
				{
						IssuerSigningKey = new SymmetricSecurityKey(new byte[8]),
						RequireAudience = false,
						RequireExpirationTime = true,
						ValidateActor = false,
						ValidateAudience = false,
						ValidateIssuer = false,
						ValidateIssuerSigningKey = true,
						ValidateLifetime = true,
						ValidateTokenReplay = false
				};
		}
}