using Microsoft.Extensions.Logging;
using ToDoList.Application.Responses;
using ToDoList.Application.UseCases;
using ToDoList.Domain.Entities.Interfaces;
using ToDoList.Infrastructure.Repositories.Interfaces;
using ToDoList.Test.Builders;

namespace ToDoList.Test.UseCases;

public class GetTasksUseCaseTest
{
    private readonly GetTasksUseCase _getTasksUseCase;
    private readonly Mock<ILogger<GetTasksUseCase>> _logger;
    private readonly Mock<ITaskRepository> _taskRepository;

    public GetTasksUseCaseTest()
    {
        _logger = new();
        _taskRepository = new();
        _getTasksUseCase = new(
            _logger.Object,
            _taskRepository.Object);
    }

    [Theory]
    [MemberData(nameof(NotFoundTasks))]
    public async Task Type_WhenTasksAreNotFound_ShouldReturnNotFound(
        IEnumerable<ITask> tasks)
    {
        // Arrange
        _taskRepository
            .Setup(x => x.GetTasks())
            .ReturnsAsync(tasks);

        // Act
        var result = await _getTasksUseCase
            .GetTasks();

        // Assert
        result.Type.ShouldBe(ResponseType.NOT_FOUND);
        result.Errors.ShouldNotBeEmpty();
    }

    public static IEnumerable<object[]> NotFoundTasks() =>
        [
            [ null! ],
            [ new List<ITask>() ]
        ];

    [Fact]
    public async Task Type_WhenTasksAreFound_ShouldReturnSuccess()
    {
        // Arrange
        var tasks = new List<ITask>()
        {
            new TaskBuilder().Build()
        };
        _taskRepository
            .Setup(x => x.GetTasks())
            .ReturnsAsync(tasks);

        // Act
        var result = await _getTasksUseCase.GetTasks();

        // Assert
        result.Type.ShouldBe(ResponseType.SUCCESS);
    }

    [Fact]
    public async Task WhenExpcetionIsThrown_ShouldReturnInternalError()
    {
        // Arrange
        var exception = new Exception("Exception");
        _taskRepository
            .Setup(x => x.GetTasks())
            .ThrowsAsync(exception);

        // Act
        var result = await _getTasksUseCase.GetTasks();

        // Assert
        result.Type.ShouldBe(ResponseType.INTERNAL_ERROR);
        _logger.VerifyLog(x => x.LogError(exception,
                "{Message}",
                exception.Message),
            Times.Once);
    }
}
