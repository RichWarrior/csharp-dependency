using csharp.dependency.core.Interface;
using csharp.dependency.service.Data;
using csharp.dependency.service.GeneralService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using System.Threading.Tasks;

namespace csharp.dependency.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Middlewares
            services.AddCors();
            services.AddControllers();
            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Latest);
            #endregion
            #region JWT Auth
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
              )
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateActor = true,
                      ValidateAudience = false,
                      ValidateIssuer = false,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(
                          Encoding.UTF8.GetBytes("csharpdependencyauth"))
                  };

                  options.Events = new JwtBearerEvents
                  {
                      OnTokenValidated = context =>
                      {
                          return Task.CompletedTask;
                      },
                      OnAuthenticationFailed = context =>
                      {
                          Console.WriteLine("Exception:{0}", context.Exception.Message);
                          return Task.CompletedTask;
                      }
                  };
              });
            #endregion
            #region Header Config
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            #endregion
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("CSharpDependency", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CSharp Dependency Api Documentation",
                    Description = "Visualize dependencies of your .NET and .NET Core Project",
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                       {
                         new OpenApiSecurityScheme
                         {
                           Reference = new OpenApiReference
                           {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                           }
                          },
                          new string[] { }
                        }
                      });

                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile));
            });
            #endregion
            #region DependencyInjection
            services.AddScoped<IDbContext, DbContext>();
            services.AddScoped<IMethod, SMethod>();
            services.AddScoped<IGithub, SGithub>();
            services.AddScoped<IUser, SUser>();
            services.AddScoped<IServer, SServer>();
            #endregion
            #region RedisService
            services.AddSingleton<SRedisService>();
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            SRedisService _SRedisService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            #region Use Middleware
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(x => x
                           .AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            app.UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/CSharpDependency/swagger.json", "CSharp Dependency");
            });
            app.UseMvc();
            #endregion
            #region Redis Connection
            _SRedisService.Connect();
            #endregion
        }
    }
}
