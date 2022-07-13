using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Emporium.POS.API.Configuration;
using Emporium.POS.API.DBContext;
using Emporium.POS.API.Interfaces;
using Emporium.POS.API.Services;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Emporium.POS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            APISettings.AuthSecret = Configuration["Security:AuthSecret"];

            APISettings.RedisConnection = Configuration["RedisSettings:RedisConnection"];
            APISettings.RedisInstanceName = Configuration["RedisSettings:RedisInstanceName"];
            APISettings.RedisPort = Configuration["RedisSettings:RedisPort"];

            APISettings.RedisPort = Configuration["Security:TokenValidityDays"];
            APISettings.RedisPort = Configuration["Security:TokenValidityMinutes"];

            APISettings.OtpByEmail = ReadBoolFromConfig("Security:OtpByEmail");
            APISettings.OtpBySms = ReadBoolFromConfig("Security:OtpBySms");
            APISettings.OtpValiditySeconds = int.Parse(Configuration["Security:OtpValiditySeconds"] ?? "300");
            APISettings.OtpLength = int.Parse(Configuration["Security:OtpLength"] ?? "4");

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {   
            ConfigureLogging(services);
            ConfigureAuthentication(services);
            ConfigureDBConnection(services);

            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IOTPService, OTPService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IUserAccountService, UserAccountService>();
            services.AddScoped<IValidateRequestDataService, ValidateRequestDataService>();
            services.AddScoped<ISKUService, SKUService>();

            services.AddMvc();

            ConfigureRedis(services);

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowAnyHeader()));
            services.Configure<MvcOptions>(options => {
                options.Filters.Add(new Microsoft.AspNetCore.Mvc.Cors.Internal.CorsAuthorizationFilterFactory("AllowAll"));
            });
        }

        private void ConfigureDBConnection(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<WebApiDBContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("WebApiDBConnection")));
        }

        public void ConfigureLogging(IServiceCollection services)
        {
            var logRepo = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepo, new FileInfo("log4net.config"));

            // Log levels: // Off // Fatal // Error // Warn // Info // Debug // All

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILoggerFactory>()
                .AddLog4Net()
                .CreateLogger<Program>();

            services.AddSingleton(typeof(ILogger), logger);
        }

        public void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", jwtBearerOptions => {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(APISettings.AuthSecret)),

                    ValidateIssuer = false,
                    //ValidIssuer = "The name of the issuer",

                    ValidateAudience = false,
                    //ValidAudience = "The name of the audience",

                    ValidateLifetime = true, 
                    //validate the expiration and not before values in the token

                    ClockSkew = TimeSpan.FromMinutes(5) 
                    //5 minute tolerance for the expiration date
                };
            });
        }

        public void ConfigureRedis(IServiceCollection services)
        {
            services.AddDistributedRedisCache(option => {
                option.Configuration = APISettings.RedisConnection;
                option.InstanceName = APISettings.RedisInstanceName;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
        }

        public bool ReadBoolFromConfig(string propertyName, bool defaultValue = false)
        {
            bool value;
            var valueParsed = bool.TryParse(Configuration[propertyName] ?? $"{defaultValue}", out value);

            if (!valueParsed)
            {
                value = defaultValue;
            }

            return value;
        }
    }
}
