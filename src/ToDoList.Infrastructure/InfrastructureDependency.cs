using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Infrastructure.Repositories;
using ToDoList.Infrastructure.Repositories.Interfaces;
using ToDoList.Infrastructure.Settings;

namespace ToDoList.Infrastructure;

public static class InfrastructureDependency
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddRepositories()
            .AddSettings(configuration);
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITaskRepository, TaskRepository>();
        return services;
    }

    private static IServiceCollection AddSettings(this IServiceCollection services,
        IConfiguration configuration)
    {
        var databaseSettings = configuration
            .GetSection(nameof(DatabaseSettings));
        ArgumentNullException.ThrowIfNull(databaseSettings);
        services.Configure<DatabaseSettings>(databaseSettings);
        return services;
    }
}
