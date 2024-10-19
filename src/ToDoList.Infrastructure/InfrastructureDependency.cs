using Microsoft.Extensions.DependencyInjection;
using ToDoList.Infrastructure.Repositories;
using ToDoList.Infrastructure.Repositories.Interfaces;

namespace ToDoList.Infrastructure;

public static class InfrastructureDependency
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddRepositories();
        return services;
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITaskRepository, TaskRepository>();
    }
}
