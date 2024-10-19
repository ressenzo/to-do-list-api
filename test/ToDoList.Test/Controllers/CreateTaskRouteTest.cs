using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Responses;
using ToDoList.Test.Builders;

namespace ToDoList.Test.Controllers;

public partial class TaskControllerTest
{
    [Fact]
    public async Task Type_WhenIsSuccess_ShouldReturnOk()
    {
        // Arrange
        var request = new CreateTaskRequestBuilder()
            .Build();
        var successResponse = new CreateTaskResponseBuilder()
            .Success()
            .Build();
        _createTaskUseCase
            .Setup(x => x.CreateTask(It.IsAny<string>()))
            .ReturnsAsync(successResponse);

        // Act
        var response  = await _taskController
            .CreateTask(request);

        // Assert
        _createTaskUseCase.Verify(x => x.CreateTask(
                It.IsAny<string>()),
            Times.Once);
        response.ShouldBeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Type_WhenIsValidationError_ShouldReturnBadRequest()
    {
        // Arrange
        var request = new CreateTaskRequestBuilder()
            .Build();
        var badRequestResponse = new CreateTaskResponseBuilder()
            .ValidationError()
            .Build();
        _createTaskUseCase
            .Setup(x => x.CreateTask(It.IsAny<string>()))
            .ReturnsAsync(badRequestResponse);

        // Act
        var response  = await _taskController
            .CreateTask(request);

        // Assert
        _createTaskUseCase.Verify(x => x.CreateTask(
                It.IsAny<string>()),
            Times.Once);
        response.ShouldBeOfType<BadRequestObjectResult>();
    }
}
