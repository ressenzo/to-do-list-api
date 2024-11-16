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
        var barRequestResponse = new UpdateTaskBuilder()
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
        badRequestResult.Value.ShouldBeOfType<Response<UpdateTaskResponse>>();
    }

    [Fact]
    public async Task SetTaskInProgressRoute_Type_WhenIsSuccess_ShouldReturnOk()
    {
        // Arrange
        var successResponse = new UpdateTaskBuilder()
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
        var okObjectResult = response
            .ShouldBeOfType<OkObjectResult>();
        okObjectResult.Value.ShouldNotBeNull();
        okObjectResult.Value
            .ShouldBeOfType<UpdateTaskResponse>();
    }

    [Fact]
    public async Task SetTaskInProgressRoute_Type_WhenIsInternalError_ShouldReturnInternalError()
    {
        // Arrange
        var internalErrorResponse = new UpdateTaskBuilder()
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
        objectResult.Value.ShouldBeOfType<Response<UpdateTaskResponse>>();
    }
}
