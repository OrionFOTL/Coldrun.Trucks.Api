using Coldrun.Trucks.Application.Abstractions.DatabaseContext;
using Coldrun.Trucks.Application.UnitOfWork;
using Coldrun.Trucks.Domain.Entities.Trucks;
using Coldrun.Trucks.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(options => options.UseSqlite(configuration.GetConnectionString("Sqlite")));

        services.AddScoped<IDatabaseContext>(sp => sp.GetRequiredService<DatabaseContext>());
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<DatabaseContext>());

        services.AddTransient<ITruckRepository, TruckRepository>();

        return services;
    }
}