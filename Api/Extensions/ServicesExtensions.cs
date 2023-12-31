using System.Text;
using Api.Services;
using Application.Contratos;
using Application.Helpers.Security;
using Application.Services;
using Application.UnitOfWork;
using AspNetCoreRateLimit;
using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using Security.Tokens;

namespace Api.Extensions;
public static class ServicesExtensions{
    public static void AddApplicationServices(this IServiceCollection services){
        services.AddScoped<IJwtGenerator,JwtGenerator>();
        services.AddScoped<IPasswordHasher<User>,PasswordHasher<User>>();
        services.AddScoped<IUserServices,UserServices>();
        services.AddScoped<IUnitOfWork,UnitOfWork>();
    }

    public static void ConfigureCors(this IServiceCollection services){
        services.AddCors(opt =>{
            opt.AddPolicy("CorsPolicy", policy =>{
                policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }


    public static void ConfigurationRatelimiting(this IServiceCollection services){
        services.AddMemoryCache();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddInMemoryRateLimiting();
        services.Configure<IpRateLimitOptions>(opt =>{
            opt.EnableEndpointRateLimiting = true;
            opt.StackBlockedRequests = false;
            opt.HttpStatusCode = 429;
            opt.RealIpHeader = "X-Real-IP";
            opt.GeneralRules = new(){
                new(){
                   Endpoint = "*",
                   Period = "10s",
                   Limit = 2
                }
            };
        });
    }

    public static void ConfigureApiVersioning(this IServiceCollection services){
        services.AddApiVersioning(opt =>{
            opt.DefaultApiVersion = new(1,0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("ver"),
                new HeaderApiVersionReader("X-Version")
            );
            opt.ReportApiVersions = true;
        });
    }

    public static void AddJwt(this IServiceCollection services, IConfiguration configuration){
        services.Configure<JWT>(configuration.GetSection("JWT"));

        services.AddAuthentication(opts => {
            opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;            
        }).AddJwtBearer(o => {
            o.RequireHttpsMetadata = false;
            o.SaveToken = false;
            o.TokenValidationParameters = new TokenValidationParameters{
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!))
            };
        });
    }
}
