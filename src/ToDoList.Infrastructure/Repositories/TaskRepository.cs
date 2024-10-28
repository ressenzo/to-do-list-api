using Microsoft.Extensions.Options;
using MongoDB.Bson;
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

    public async Task<ITask?> GetTask(string id)
    {
        var taskModel = await _taskCollection
            .Find(x => x.Id.Equals(id))
            .FirstOrDefaultAsync();
        return taskModel?.ToEntity();
    }

    public async Task<IEnumerable<ITask>?> GetTasks()
    {
        var tasks = await _taskCollection
            .Find(new BsonDocument())
            .ToListAsync();

        return tasks?
            .Select(x => x.ToEntity());
    }

    public async Task<bool> UpdateTask(ITask task)
    {
        var taskModel = TaskModel.Construct(task);
        var filter = Builders<TaskModel>
            .Filter
            .Eq(x => x.Id, taskModel.Id);
        var replaceResult = await _taskCollection
            .ReplaceOneAsync(filter, taskModel);
        return replaceResult.ModifiedCount > 0;
    }
}
