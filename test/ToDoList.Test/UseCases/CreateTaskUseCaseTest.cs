using Moq;
using ToDoList.Application.Responses;
using ToDoList.Application.UseCases;
using ToDoList.Domain.Entities.Interfaces;
using ToDoList.Domain.Factories.Interfaces;

namespace ToDoList.Test.UseCases;

public class CreateTaskUseCaseTest
{
    private readonly CreateTaskUseCase _createTaskUseCase;
    private readonly Mock<ITaskFactory> _taskFactory;
    private readonly Mock<ITask> _task;

    public CreateTaskUseCaseTest()
    {
        _taskFactory = new();
        _createTaskUseCase = new(
            _taskFactory.Object);
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

        // Should
        _taskFactory.Verify(x => x.Factory(
                It.Is<string>(x => x == null)),
            Times.Once);
        _task.Verify(x => x.IsValid(),
            Times.Once);
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

        // Should
        _taskFactory.Verify(x => x.Factory(
                It.Is<string>(x => x.Equals(validDescription))),
            Times.Once);
        _task.Verify(x => x.IsValid(),
            Times.Once);
        result.IsSuccess.ShouldBeTrue();
        result.Type.ShouldBe(ResponseType.SUCCESS);
        result.Content.ShouldNotBeNull();
        result.Errors.ShouldBeEmpty();
    }
}
