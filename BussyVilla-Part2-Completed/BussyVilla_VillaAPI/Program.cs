
using Asp.Versioning;
using BussyVilla_VillaAPI.Data;
using BussyVilla_VillaAPI.Extensions;
using BussyVilla_VillaAPI.Filters;
using BussyVilla_VillaAPI.Middlewares;
using BussyVilla_VillaAPI.Models;
using BussyVilla_VillaAPI.Repository;
using BussyVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace BussyVilla_VillaAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
			//   .WriteTo.File("log/villaLogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();

			//builder.Host.UseSerilog();

			builder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"),
					sqlServerOptionsAction: sqlOptions =>
					{
						sqlOptions.EnableRetryOnFailure(
							maxRetryCount: 5,
							maxRetryDelay: TimeSpan.FromSeconds(30),
							errorNumbersToAdd: null);
					});
			});

			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>();

			builder.Services.AddResponseCaching();

			builder.Services.AddScoped<IVillaRepository, VillaRepository>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();
			builder.Services.AddAutoMapper(typeof(MappingConfig));

			builder.Services.AddApiVersioning(options =>
			{
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.DefaultApiVersion = new ApiVersion(1, 0);
				options.ReportApiVersions = true;
			}).AddApiExplorer(options =>
			{
				options.GroupNameFormat = "'v'VVV";
				options.SubstituteApiVersionInUrl = true;
			});

			var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");
			builder.Services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(x =>
				{
					x.RequireHttpsMetadata = false;
					x.SaveToken = true;
					x.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
						ValidateIssuer = true,
						ValidIssuer = "https://bussyvilla-api.com",
						ValidAudience = "https://test-bussy-api.com",
						ValidateAudience = true,
						ClockSkew = TimeSpan.Zero,
					};
				});

			builder.Services.AddControllers(option =>
			{
				option.Filters.Add<CustomExceptionFilter>();
			}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters()
			.ConfigureApiBehaviorOptions(option =>
			{
				option.ClientErrorMapping[StatusCodes.Status500InternalServerError] = new ClientErrorData
				{
					Link = "https://BussyVilla.com/500"
				};
			});

			builder.Services.AddEndpointsApiExplorer();

			builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
			builder.Services.AddSwaggerGen();


			var app = builder.Build();

			// Configure the HTTP request pipeline.   
			app.UseSwagger();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwaggerUI(options =>
				{
					options.SwaggerEndpoint("/swagger/v2/swagger.json", "Bussy_VillaV2");
					options.SwaggerEndpoint("/swagger/v1/swagger.json", "Bussy_VillaV1");
				});
			}
			else
			{
				app.UseSwaggerUI(options =>
				{
					options.SwaggerEndpoint("/swagger/v1/swagger.json", "Bussy_VillaV1");
					options.SwaggerEndpoint("/swagger/v2/swagger.json", "Bussy_VillaV2");
					options.RoutePrefix = String.Empty;
				});
			}

			//app.UseExceptionHandler("/ErrorHandling/ProcessError");
			//app.HandleError(app.Environment.IsDevelopment());
			app.UseMiddleware<CustomExceptionMiddleware>();

			app.UseStaticFiles();
			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();
			app.MapControllers();
			ApplyMigration();
			app.Run();

			void ApplyMigration()
			{
				using (var scope = app.Services.CreateScope())
				{
					var _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

					if (_db.Database.GetPendingMigrations().Count() > 0)
					{
						_db.Database.Migrate();
					}
				}
			}
		}
	}
}
