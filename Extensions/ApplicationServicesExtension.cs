using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using RealEstateBank.Data;
using RealEstateBank.Data.Enums;
using RealEstateBank.Helpers;
using RealEstateBank.Interface;
using RealEstateBank.Repository;

namespace RealEstateBank.Extensions;

public static class ApplicationServicesExtension {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config) {
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection"))
        );

        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.Scan(scan => scan
            .FromAssemblyOf<Program>()
            .AddClasses()
            .AsMatchingInterface()
            .WithScopedLifetime()
        );

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
            options.IncludeErrorDetails = true;
            options.MapInboundClaims = false;
            options.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["Jwt:Issuer"],
                ValidAudience = config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(config["Jwt:Key"]!)
                )
            };
            options.Events = new JwtBearerEvents {
                OnAuthenticationFailed = context => {
                    Console.WriteLine($"JWT Auth Failed: {context.Exception}");
                    return Task.CompletedTask;
                }
            };
        });

        services.AddAuthorization(options => {
            options.AddPolicy(Policies.RequireAuthenticated, policy =>
                policy.RequireAuthenticatedUser()
            );

            options.AddPolicy(Policies.RequireUser, policy =>
                policy.RequireRole(nameof(UserRole.User))
            );

            options.AddPolicy(Policies.RequirePublisher, policy =>
                policy.RequireRole(nameof(UserRole.Publisher))
            );

            options.AddPolicy(Policies.RequireAdmin, policy =>
                policy.RequireRole(nameof(UserRole.Admin))
            );

            options.AddPolicy(Policies.RequireSuperAdmin, policy =>
                policy.RequireRole(nameof(UserRole.SuperAdmin))
            );

            options.AddPolicy(Policies.RequirePublisherOrAbove, policy =>
                policy.RequireRole(nameof(UserRole.Publisher), nameof(UserRole.Admin), nameof(UserRole.SuperAdmin))
            );

            options.AddPolicy(Policies.RequireAdminOrAbove, policy =>
                policy.RequireRole(nameof(UserRole.Admin), nameof(UserRole.SuperAdmin))
            );

            options.AddPolicy(Policies.RequireSuperAdminOnly, policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole(nameof(UserRole.SuperAdmin)) &&
                    !context.User.IsInRole(nameof(UserRole.Admin))
                )
            );

            options.AddPolicy(Policies.RequirePublisherOrAdmin, policy =>
                policy.RequireRole(nameof(UserRole.Publisher), nameof(UserRole.Admin))
            );
        });

        services.AddHttpLogging(options => {
            // Log all fields for debugging
            options.LoggingFields = HttpLoggingFields.All;

            options.RequestBodyLogLimit = 4096; // in bytes
            options.ResponseBodyLogLimit = 4096; // in bytes

            // Be careful with this, especially in production, as it can be a security risk
            // You can also add specific headers you want to log
            options.RequestHeaders.Add("Authorization");
        });

        var serviceProvider = services.BuildServiceProvider();

        return services;
    }
}
