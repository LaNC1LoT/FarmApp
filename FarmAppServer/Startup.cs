using FarmApp.Domain.Interfaces.UnitOfWorks;
using FarmApp.Infrastructure.Data.Contexts;
using FarmApp.Infrastructure.Data.UnitOfWorks;
using FarmAppServer.ServiceModels;
using FarmAppServer.Services;
using FarmAppServer.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using System.Text.Json.Serialization;

namespace FarmAppServer
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
            services.Configure<ApplicationSetting>(Configuration.GetSection("ApplicationSettings"));
            string connection = Configuration.GetConnectionString("FarmAppContext");
            services.AddDbContext<FarmAppContext>(options => options.UseSqlServer(connection));

            services.AddScoped<ICustomLogger, CustomLogger>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<UserService>();


            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FarmApp", Version = "v1" });
            });

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, FarmAppContext farmAppContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            farmAppContext.Database.Migrate();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            app.UseAuthentication();

            app.UseCors(builder => builder.WithOrigins(Configuration["ApplicationSettings:Client_URL"].ToString()).AllowAnyHeader().AllowAnyMethod());

            app.UseSwagger();
            //app.UseMiddleware<ErrorHandlingMiddleware>();
            //app.UseMiddleware<ValidationMiddleware>();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FarmApp API V1");
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
