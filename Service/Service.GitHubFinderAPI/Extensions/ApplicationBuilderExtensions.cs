using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Service.GitHubFinderAPI.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAppAuthetication(this WebApplicationBuilder builder)
    {
        var secret = builder.Configuration.GetValue<string>("ApiSettings:Secret");
        var issuer = builder.Configuration.GetValue<string>("ApiSettings:Issuer");
        var audience = builder.Configuration.GetValue<string>("ApiSettings:Audience");

        var key = Encoding.ASCII.GetBytes(secret);

        builder.Services.AddAuthentication(b =>
            {
                b.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                b.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience
                };
#if DEBUG
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                        Log.Information($"Authentication Token: {token}");
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                        Log.Warning($"Authentication Challenge: {token}");
                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                        Log.Warning($"Authentication Forbidden: {context.HttpContext.Request.Path}");
                        return Task.CompletedTask;
                    }
                };
#endif
            });

        return builder;
    }
    public static WebApplicationBuilder AddAppLogger(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();
        builder.Services.AddLogging(logging =>
        {
            logging.AddConsole();
            logging.AddDebug();
            logging.AddSerilog();
        });

        return builder;
    }
}
