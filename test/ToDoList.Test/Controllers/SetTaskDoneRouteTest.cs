using System.Net;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Responses;
using ToDoList.Test.Builders;

namespace ToDoList.Test.Controllers;

public partial class TaskControllerTest
{
    [Fact]
    public async Task SetTaskDoneRoute_Type_WhenIsValidationError_ShouldReturnBadRequest()
    {
        // Arrange
        var barRequestResponse = new SetTaskStatusBuilder()
            .ValidationError()
            .Build();
        _setTaskDoneUseCase
            .Setup(x => x.SetTaskDone(It.IsAny<string>()))
            .ReturnsAsync(barRequestResponse);

        // Act
        var response = await _taskController.SetTaskDone(
            id: "abc");

        // Assert
        _setTaskDoneUseCase.Verify(x => x.SetTaskDone(
                It.IsAny<string>()),
            Times.Once);
        var badRequestResult = response
            .ShouldBeOfType<BadRequestObjectResult>();
        badRequestResult.Value.ShouldNotBeNull();
        badRequestResult.Value.ShouldBeOfType<Response>();
    }

    [Fact]
    public async Task SetTaskDoneRoute_Type_WhenIsSuccess_ShouldReturnNoContent()
    {
        // Arrange
        var successResponse = new SetTaskStatusBuilder()
            .Success()
            .Build();
        _setTaskDoneUseCase
            .Setup(x => x.SetTaskDone(It.IsAny<string>()))
            .ReturnsAsync(successResponse);

        // Act
        var response = await _taskController.SetTaskDone(
            id: "abc");

        // Assert
        _setTaskDoneUseCase.Verify(x => x.SetTaskDone(
                It.IsAny<string>()),
            Times.Once);
        response.ShouldBeOfType<NoContentResult>();
    }

    [Fact]
    public async Task SetTaskDoneRoute_Type_WhenIsInternalError_ShouldReturnBadRequest()
    {
        // Arrange
        var internalErrorResponse = new SetTaskStatusBuilder()
            .InternalError()
            .Build();
        _setTaskDoneUseCase
            .Setup(x => x.SetTaskDone(It.IsAny<string>()))
            .ReturnsAsync(internalErrorResponse);

        // Act
        var response  = await _taskController
            .SetTaskDone(id: "abc");

        // Assert
        _setTaskDoneUseCase.Verify(x => x.SetTaskDone(
                It.IsAny<string>()),
            Times.Once);
        var objectResult = response
            .ShouldBeOfType<ObjectResult>();
        objectResult.StatusCode
            .ShouldBe((int)HttpStatusCode.InternalServerError);
        objectResult.Value.ShouldNotBeNull();
        objectResult.Value.ShouldBeOfType<Response>();
    }
}
