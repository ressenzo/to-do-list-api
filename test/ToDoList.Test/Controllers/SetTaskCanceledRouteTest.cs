using System.Net;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Responses;
using ToDoList.Test.Builders;

namespace ToDoList.Test.Controllers;

public partial class TaskControllerTest
{
    [Fact]
    public async Task SetTaskCanceledRoute_Type_WhenIsValidationError_ShouldReturnBadRequest()
    {
        // Arrange
        var barRequestResponse = new SetTaskStatusBuilder()
            .ValidationError()
            .Build();
        _setTaskCanceledUseCase
            .Setup(x => x.SetTaskCanceled(It.IsAny<string>()))
            .ReturnsAsync(barRequestResponse);

        // Act
        var response = await _taskController.SetTaskCanceled(
            id: "abc");

        // Assert
        _setTaskCanceledUseCase.Verify(x => x.SetTaskCanceled(
                It.IsAny<string>()),
            Times.Once);
        var badRequestResult = response
            .ShouldBeOfType<BadRequestObjectResult>();
        badRequestResult.Value.ShouldNotBeNull();
        badRequestResult.Value.ShouldBeOfType<Response>();
    }

    [Fact]
    public async Task SetTaskCanceledRoute_Type_WhenIsSuccess_ShouldReturnNoContent()
    {
        // Arrange
        var successResponse = new SetTaskStatusBuilder()
            .Success()
            .Build();
        _setTaskCanceledUseCase
            .Setup(x => x.SetTaskCanceled(It.IsAny<string>()))
            .ReturnsAsync(successResponse);

        // Act
        var response = await _taskController.SetTaskCanceled(
            id: "abc");

        // Assert
        _setTaskCanceledUseCase.Verify(x => x.SetTaskCanceled(
                It.IsAny<string>()),
            Times.Once);
        response.ShouldBeOfType<NoContentResult>();
    }

    [Fact]
    public async Task SetTaskCanceledRoute_Type_WhenIsInternalError_ShouldReturnInternalError()
    {
        // Arrange
        var internalErrorResponse = new SetTaskStatusBuilder()
            .InternalError()
            .Build();
        _setTaskCanceledUseCase
            .Setup(x => x.SetTaskCanceled(It.IsAny<string>()))
            .ReturnsAsync(internalErrorResponse);

        // Act
        var response  = await _taskController
            .SetTaskCanceled(id: "abc");

        // Assert
        _setTaskCanceledUseCase.Verify(x => x.SetTaskCanceled(
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
