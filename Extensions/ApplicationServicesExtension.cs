using Microsoft.EntityFrameworkCore;
using RealEstateBank.Data;
using RealEstateBank.Helpers;
using RealEstateBank.Interface;
using RealEstateBank.Repository;

namespace RealEstateBank.Extensions;

public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
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

        var serviceProvider = services.BuildServiceProvider();

        return services;
    }

}
