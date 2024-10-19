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
        services.AddRepositories();
        services.AddSettings(configuration);
        return services;
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITaskRepository, TaskRepository>();
    }

    private static void AddSettings(this IServiceCollection services,
        IConfiguration configuration)
    {
        var databaseSettings = configuration
            .GetSection(nameof(DatabaseSettings));
        services.Configure<DatabaseSettings>(databaseSettings);
    }
}
