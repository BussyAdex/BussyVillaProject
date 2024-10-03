
using Asp.Versioning;
using BussyVilla_VillaAPI.Data;
using BussyVilla_VillaAPI.Models;
using BussyVilla_VillaAPI.Repository;
using BussyVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
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
                options.DefaultApiVersion = new ApiVersion(1,0);
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
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            
            builder.Services.AddControllers(option =>
            {
                option.CacheProfiles.Add("Default30",
                    new CacheProfile()
                    {
                        Duration = 30
                    });
            }).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

            builder.Services.AddEndpointsApiExplorer();
            
            builder.Services.AddSwaggerGen(options =>
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
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "Bussy Villa V1",
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
                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2.0",
                    Title = "Bussy Villa V2",
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
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.   
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Bussy_VillaV1");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "Bussy_VillaV2");
                options.RoutePrefix = String.Empty;
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
