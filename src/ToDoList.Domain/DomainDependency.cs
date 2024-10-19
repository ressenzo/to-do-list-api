using Microsoft.Extensions.DependencyInjection;
using ToDoList.Domain.Factories.Interfaces;
using TaskFactory = ToDoList.Domain.Factories.TaskFactory;

namespace ToDoList.Domain;

public static class DomainDependency
{
    public static IServiceCollection AddDomain(
        this IServiceCollection services)
    {
        services.AddFactories();
        return services;
    }

    

    private static void AddFactories(
        this IServiceCollection services)
    {
        services.AddSingleton<ITaskFactory, TaskFactory>();
    }
}
