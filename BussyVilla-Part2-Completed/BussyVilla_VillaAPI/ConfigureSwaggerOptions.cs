using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BussyVilla_VillaAPI
{
	public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
	{
		readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        public void Configure(SwaggerGenOptions options)
		{
			options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
			{
				Description =
						"JWT Authorization header using the Bearer scheme. \r\n\r\n " +
						"Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\n" +
						"Example: \"Bearer 1234abcdef\"",
				Name = "Authorization",
				In = ParameterLocation.Header,
				Scheme = "Bearer"
			});
			options.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							},
							Scheme = "Oauth2",
							Name = "Bearer",
							In = ParameterLocation.Header

						},
						new List<string>()
					}

				});

			foreach(var desc in provider.ApiVersionDescriptions)
			{
				options.SwaggerDoc(desc.GroupName, new OpenApiInfo
				{
					Version = desc.ApiVersion.ToString(),
					Title = $"Bussy Villa {desc.ApiVersion}",
					Description = "API to manage Villa",
					TermsOfService = new Uri("http://bussy.com/terms"),
					Contact = new OpenApiContact()
					{
						Name = "Bussy Adex",
						Url = new Uri("http://bussy.com/terms"),
					},
					License = new OpenApiLicense
					{
						Name = "Bussy License",
						Url = new Uri("http://bussy.com/terms"),
					}
				});
			}

		}
	}
}
