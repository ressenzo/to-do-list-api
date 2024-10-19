using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoList.Domain.Entities.Interfaces;
using ToDoList.Infrastructure.Models;
using ToDoList.Infrastructure.Repositories.Interfaces;
using ToDoList.Infrastructure.Settings;

namespace ToDoList.Infrastructure.Repositories;

internal class TaskRepository : ITaskRepository
{
    private readonly IMongoCollection<TaskModel> _taskCollection;

    public TaskRepository(IOptions<DatabaseSettings> options)
    {
        var databaseSettings = options.Value;
        var client = new MongoClient(
            databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.Name);
        _taskCollection = database.GetCollection<TaskModel>(
            databaseSettings.Collection);
    }

    public async Task CreateTask(ITask task)
    {
        var taskModel = TaskModel.Construct(task);
        await _taskCollection.InsertOneAsync(taskModel);
    }
}
