using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.UseCases;
using ToDoList.Application.UseCases.Interfaces;

namespace ToDoList.Application;

public static class ApplicationDependency
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddUseCases();
        return services;
    }

    private static void AddUseCases(
        this IServiceCollection services)
    {
        services
            .AddScoped<ICreateTaskUseCase, CreateTaskUseCase>()
            .AddScoped<ISetTaskInProgressUseCase, SetTaskInProgressUseCase>()
            .AddScoped<IGetTasksUseCase, GetTasksUseCase>()
            .AddScoped<ISetTaskDoneUseCase, SetTaskDoneUseCase>()
            .AddScoped<ISetTaskCanceledUseCase, SetTaskCanceledUseCase>();
    }
}
