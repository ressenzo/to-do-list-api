using System.Net;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Responses;
using ToDoList.Test.Builders;

namespace ToDoList.Test.Controllers;

public partial class TaskControllerTest
{
    [Fact]
    public async Task CreateTaskRoute_Type_WhenIsSuccess_ShouldReturnCreated()
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
        var createdResult = response
            .ShouldBeOfType<CreatedResult>();
        createdResult.Location.ShouldNotBeEmpty();
        createdResult.Value.ShouldNotBeNull();
        createdResult.Value
            .ShouldBeOfType<CreateTaskResponse>();
    }

    [Fact]
    public async Task CreateTaskRoute_Type_WhenIsValidationError_ShouldReturnBadRequest()
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
        var badRequestResult = response
            .ShouldBeOfType<BadRequestObjectResult>();
        badRequestResult.Value.ShouldNotBeNull();
        badRequestResult.Value
            .ShouldBeOfType<Response<CreateTaskResponse>>();
    }

    [Fact]
    public async Task CreateTaskRoute_Type_WhenIsInternalError_ShouldReturn500()
    {
        // Arrange
        var request = new CreateTaskRequestBuilder()
            .Build();
        var internalErrorResponse = new CreateTaskResponseBuilder()
            .InternalError()
            .Build();
        _createTaskUseCase
            .Setup(x => x.CreateTask(It.IsAny<string>()))
            .ReturnsAsync(internalErrorResponse);

        // Act
        var response  = await _taskController
            .CreateTask(request);

        // Assert
        _createTaskUseCase.Verify(x => x.CreateTask(
                It.IsAny<string>()),
            Times.Once);
        var objectResult = response
            .ShouldBeOfType<ObjectResult>();
        objectResult.StatusCode
            .ShouldBe((int)HttpStatusCode.InternalServerError);
        objectResult.Value.ShouldNotBeNull();
        objectResult.Value
            .ShouldBeOfType<Response<CreateTaskResponse>>();
    }
}
