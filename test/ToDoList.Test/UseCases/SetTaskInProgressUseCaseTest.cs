using Microsoft.Extensions.Logging;
using ToDoList.Application.Responses;
using ToDoList.Application.UseCases;
using ToDoList.Domain.Entities.Interfaces;
using ToDoList.Infrastructure.Repositories.Interfaces;

namespace ToDoList.Test.UseCases;

public class SetTaskInProgressUseCaseTest
{
    private readonly SetTaskInProgressUseCase _setTaskInProgressUseCase;
    private readonly Mock<ILogger<SetTaskInProgressUseCase>> _logger;
    private readonly Mock<ITaskRepository> _taskRepository;
    private readonly Mock<ITask> _task;

    public SetTaskInProgressUseCaseTest()
    {
        _logger = new();
        _taskRepository = new();
        _setTaskInProgressUseCase = new(
            _logger.Object,
            _taskRepository.Object);
        _task = new();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    [InlineData("123456789")]
    public async Task Id_WhenIsInvalidValue_ShouldReturnValidationError(
        string id)
    {
        // Arrange - Act
        var result = await _setTaskInProgressUseCase
            .SetTaskInProgress(id);

        // Assert
        result.Type.ShouldBe(ResponseType.VALIDATION_ERROR);
        result.Errors.ShouldNotBeEmpty();
        _taskRepository
            .Verify(x => x.GetTask(It.IsAny<string>()),
                Times.Never);
        _taskRepository
            .Verify(x => x.UpdateTask(It.IsAny<ITask>()),
                Times.Never);
    }

    [Fact]
    public async Task Task_WhenIsNotFound_ShouldReturnNotFound()
    {
        // Arrange
        _taskRepository
            .Setup(x => x.GetTask(It.IsAny<string>()))
            .ReturnsAsync((ITask)null!);

        // Act
        var result = await _setTaskInProgressUseCase
            .SetTaskInProgress(id: "12345");

        // Assert
        result.Type.ShouldBe(ResponseType.NOT_FOUND);
        result.Errors.ShouldNotBeEmpty();
        _taskRepository
            .Verify(x => x.GetTask(It.IsAny<string>()),
                Times.Once);
        _taskRepository
            .Verify(x => x.UpdateTask(It.IsAny<ITask>()),
                Times.Never);
    }

    [Theory]
    [InlineData(true, ResponseType.SUCCESS)]
    [InlineData(false, ResponseType.INTERNAL_ERROR)]
    public async Task Task_WhenIsFound_ShouldSetAsInProgressAndUpdate(
        bool updateResult,
        ResponseType responseType)
    {
        // Arrange
        _task.Setup(x => x.Id)
            .Returns("12345");
        _taskRepository
            .Setup(x => x.GetTask(It.IsAny<string>()))
            .ReturnsAsync(_task.Object);
        _taskRepository
            .Setup(x => x.UpdateTask(It.IsAny<ITask>()))
            .ReturnsAsync(updateResult);

        // Act
        var result = await _setTaskInProgressUseCase
            .SetTaskInProgress(id: "12345");

        // Assert
        result.Type.ShouldBe(responseType);
        if (updateResult)
        {
            result.Content.ShouldNotBeNull();
            result.Errors.ShouldBeEmpty();
        }
        else
            result.Errors.ShouldNotBeEmpty();
        _taskRepository
            .Verify(x => x.GetTask(It.IsAny<string>()),
                Times.Once);
        _task
            .Verify(x => x.SetAsInProgress(), Times.Once);
        _taskRepository
            .Verify(x => x.UpdateTask(It.IsAny<ITask>()),
                Times.Once);
    }

    [Fact]
    public async Task WhenExpcetionIsThrown_ShouldReturnInternalError()
    {
        // Arrange
        _task.Setup(x => x.Id)
            .Returns("12345");
        var exception = new Exception("Exception");
        _taskRepository
            .Setup(x => x.GetTask(It.IsAny<string>()))
            .ThrowsAsync(exception);

        // Act
        var result = await _setTaskInProgressUseCase
            .SetTaskInProgress(id: "12345");

        // Assert
        result.Type.ShouldBe(ResponseType.INTERNAL_ERROR);
        _logger.VerifyLog(x => x.LogError(exception,
                "{Message}",
                exception.Message),
            Times.Once);
    }
}
