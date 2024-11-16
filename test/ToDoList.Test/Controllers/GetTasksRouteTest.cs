using System.Net;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Responses;
using ToDoList.Test.Builders;

namespace ToDoList.Test.Controllers;

public partial class TaskControllerTest
{
    [Fact]
    public async Task GetTasksRoute_Type_WhenIsSuccess_ShouldReturnOk()
    {
        // Arrange
        var successResponse = new GetTasksResponseBuilder()
            .Success()
            .Build();
        _getTasksUseCase
            .Setup(x => x.GetTasks())
            .ReturnsAsync(successResponse);

        // Act
        var response = await _taskController
            .GetTasks();

        // Assert
        _getTasksUseCase
            .Verify(x => x.GetTasks(),
                Times.Once);
        var okObjectResult = response
            .ShouldBeOfType<OkObjectResult>();
        okObjectResult.Value.ShouldNotBeNull();
        okObjectResult.Value
            .ShouldBeOfType<GetTasksResponse>();
    }

    [Fact]
    public async Task GetTasksRoute_Type_WhenIsNotFound_ShouldReturnOk()
    {
        // Arrange
        var notFoundResponse = new GetTasksResponseBuilder()
            .NotFound()
            .Build();
        _getTasksUseCase
            .Setup(x => x.GetTasks())
            .ReturnsAsync(notFoundResponse);

        // Act
        var response = await _taskController
            .GetTasks();

        // Assert
        _getTasksUseCase
            .Verify(x => x.GetTasks(),
                Times.Once);
        var notFoundResult = response
            .ShouldBeOfType<OkObjectResult>();
        notFoundResult.Value.ShouldNotBeNull();
        var getTasksResponse = notFoundResult.Value
            .ShouldBeOfType<GetTasksResponse>();
        getTasksResponse.Tasks.ShouldBeEmpty();
    }

    [Fact]
    public async Task GetTasksRoute_Type_WhenIsInternalError_ShouldReturnInternalServerError()
    {
        // Arrange
        var internalErrorResponse = new GetTasksResponseBuilder()
            .InternalError()
            .Build();
        _getTasksUseCase
            .Setup(x => x.GetTasks())
            .ReturnsAsync(internalErrorResponse);

        // Act
        var response = await _taskController
            .GetTasks();

        // Assert
        _getTasksUseCase
            .Verify(x => x.GetTasks(),
                Times.Once);
        var objectResult = response
            .ShouldBeOfType<ObjectResult>();
        objectResult.StatusCode
            .ShouldBe((int)HttpStatusCode.InternalServerError);
        objectResult.Value.ShouldNotBeNull();
        objectResult.Value
            .ShouldBeOfType<Response<GetTasksResponse>>();
    }
}
