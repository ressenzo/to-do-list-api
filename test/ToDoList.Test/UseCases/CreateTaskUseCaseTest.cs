using Microsoft.Extensions.Logging;
using ToDoList.Application.Responses;
using ToDoList.Application.UseCases;
using ToDoList.Domain.Entities.Interfaces;
using ToDoList.Domain.Factories.Interfaces;
using ToDoList.Infrastructure.Repositories.Interfaces;

namespace ToDoList.Test.UseCases;

public class CreateTaskUseCaseTest
{
    private readonly CreateTaskUseCase _createTaskUseCase;
    private readonly Mock<ILogger<CreateTaskUseCase>> _logger;
    private readonly Mock<ITaskFactory> _taskFactory;
    private readonly Mock<ITaskRepository> _taskRepository;
    private readonly Mock<ITask> _task;

    public CreateTaskUseCaseTest()
    {
        _logger = new();
        _taskFactory = new();
        _taskRepository = new();
        _createTaskUseCase = new(
            _logger.Object,
            _taskFactory.Object,
            _taskRepository.Object);
        _task = new();
    }

    [Fact]
    public async Task Task_WhenIsInvalid_ShouldReturnValidationError()
    {
        // Arrange
        _task.Setup(x => x.IsValid())
            .Returns(false);
        _task.Setup(x => x.Errors)
            .Returns(["Error"]);
        _taskFactory
            .Setup(x => x.Factory(
                It.IsAny<string>()))
            .Returns(_task.Object);
        string? invalidDescription = null!;

        // Act
        var result = await _createTaskUseCase
            .CreateTask(invalidDescription);

        // Assert
        _taskFactory.Verify(x => x.Factory(
                It.Is<string>(x => x == null)),
            Times.Once);
        _task.Verify(x => x.IsValid(),
            Times.Once);
        _taskRepository.Verify(x => x.CreateTask(
                It.IsAny<ITask>()),
            Times.Never);
        result.IsSuccess.ShouldBeFalse();
        result.Type.ShouldBe(ResponseType.VALIDATION_ERROR);
        result.Content.ShouldBeNull();
        result.Errors.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task Task_WhenIsValid_ShouldReturnSuccessAndContentNotNull()
    {
        // Arrange
        _task.Setup(x => x.IsValid())
            .Returns(true);
        _taskFactory
            .Setup(x => x.Factory(
                It.IsAny<string>()))
            .Returns(_task.Object);
        string validDescription = "Task";

        // Act
        var result = await _createTaskUseCase
            .CreateTask(validDescription);

        // Assert
        _taskFactory.Verify(x => x.Factory(
                It.Is<string>(x => x.Equals(validDescription))),
            Times.Once);
        _task.Verify(x => x.IsValid(),
            Times.Once);
        _taskRepository.Verify(x => x.CreateTask(
                _task.Object),
            Times.Once);
        result.IsSuccess.ShouldBeTrue();
        result.Type.ShouldBe(ResponseType.SUCCESS);
        result.Content.ShouldNotBeNull();
        result.Errors.ShouldBeEmpty();
    }

    [Fact]
    public async Task WhenExpcetionIsThrown_ShouldReturnInternalError()
    {
        // Arrange
        _task.Setup(x => x.IsValid())
            .Returns(true);
        _taskFactory
            .Setup(x => x.Factory(
                It.IsAny<string>()))
            .Returns(_task.Object);
        string validDescription = "Task";
        var exception = new Exception("Exception");
        _taskRepository
            .Setup(x => x.CreateTask(It.IsAny<ITask>()))
            .ThrowsAsync(exception);

        // Act
        var result = await _createTaskUseCase
            .CreateTask(validDescription);

        // Assert
        result.Type.ShouldBe(ResponseType.INTERNAL_ERROR);
        _logger.VerifyLog(x => x.LogError(exception,
                "{Message}",
                exception.Message),
            Times.Once);
    }
}
