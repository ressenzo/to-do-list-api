using System.Net;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Responses;
using ToDoList.Test.Builders;

namespace ToDoList.Test.Controllers;

public partial class TaskControllerTest
{
    [Fact]
    public async Task SetTaskInProgressRoute_Type_WhenIsValidationError_ShouldReturnBadRequest()
    {
        // Arrange
        var barRequestResponse = new SetTaskInProgressResponseBuilder()
            .ValidationError()
            .Build();
        _setTaskInProgressUseCase
            .Setup(x => x.SetTaskInProgress(It.IsAny<string>()))
            .ReturnsAsync(barRequestResponse);

        // Act
        var response = await _taskController.SetTaskInProgress(
            id: "abc");

        // Assert
        _setTaskInProgressUseCase.Verify(x => x.SetTaskInProgress(
                It.IsAny<string>()),
            Times.Once);
        var badRequestResult = response
            .ShouldBeOfType<BadRequestObjectResult>();
        badRequestResult.Value.ShouldNotBeNull();
        badRequestResult.Value.ShouldBeOfType<Response>();
    }

    [Fact]
    public async Task SetTaskInProgressRoute_Type_WhenIsSuccess_ShouldReturnBadRequest()
    {
        // Arrange
        var successResponse = new SetTaskInProgressResponseBuilder()
            .Success()
            .Build();
        _setTaskInProgressUseCase
            .Setup(x => x.SetTaskInProgress(It.IsAny<string>()))
            .ReturnsAsync(successResponse);

        // Act
        var response = await _taskController.SetTaskInProgress(
            id: "abc");

        // Assert
        _setTaskInProgressUseCase.Verify(x => x.SetTaskInProgress(
                It.IsAny<string>()),
            Times.Once);
        response.ShouldBeOfType<NoContentResult>();
    }

    [Fact]
    public async Task SetTaskInProgressRoute_Type_WhenIsInternalError_ShouldReturnBadRequest()
    {
        // Arrange
        var internalErrorResponse = new SetTaskInProgressResponseBuilder()
            .InternalError()
            .Build();
        _setTaskInProgressUseCase
            .Setup(x => x.SetTaskInProgress(It.IsAny<string>()))
            .ReturnsAsync(internalErrorResponse);

        // Act
        var response  = await _taskController
            .SetTaskInProgress(id: "abc");

        // Assert
        _setTaskInProgressUseCase.Verify(x => x.SetTaskInProgress(
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
