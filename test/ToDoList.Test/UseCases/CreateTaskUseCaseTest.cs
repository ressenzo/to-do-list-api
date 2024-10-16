using ToDoList.Application.Responses;
using ToDoList.Application.UseCases;

namespace ToDoList.Test.UseCases;

public class CreateTaskUseCaseTest
{
    private readonly CreateTaskUseCase _createTaskUseCase;

    public CreateTaskUseCaseTest()
    {
        _createTaskUseCase = new();
    }

    [Fact]
    public async Task Task_WhenIsInvalid_ShouldReturnValidationError()
    {
        // Arrange
        string? invalidDescription = null!;

        // Act
        var result = await _createTaskUseCase
            .CreateTask(invalidDescription);

        // Should
        result.IsSuccess.ShouldBeFalse();
        result.Type.ShouldBe(ResponseType.VALIDATION_ERROR);
        result.Content.ShouldBeNull();
        result.Errors.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task Task_WhenIsValid_ShouldReturnSuccessAndContentNotNull()
    {
        // Arrange
        string validDescription = "Task";

        // Act
        var result = await _createTaskUseCase
            .CreateTask(validDescription);

        // Should
        result.IsSuccess.ShouldBeTrue();
        result.Type.ShouldBe(ResponseType.SUCCESS);
        result.Content.ShouldNotBeNull();
        result.Errors.ShouldBeEmpty();
    }
}
