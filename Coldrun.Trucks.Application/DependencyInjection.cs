using Coldrun.Trucks.Application.Trucks.Get.Many.GraphQL;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services
            .AddGraphQLServer()
            .AddQueryType<GetTrucksGraphQLQuery>()
            .AddProjections()
            .AddFiltering()
            .AddSorting();

        return services;
    }
}
