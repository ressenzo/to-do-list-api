using System.Net;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Requests;
using ToDoList.Application.Responses;
using ToDoList.Application.UseCases.Interfaces;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TaskController(
    ICreateTaskUseCase createTaskUseCase,
    ISetTaskInProgressUseCase setTaskInProgressUseCase) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTask(
        CreateTaskRequest request)
    {
        var result = await createTaskUseCase
            .CreateTask(request.Description);
        
        return result.Type switch
        {
            ResponseType.SUCCESS => Created($"api/tasks/{result.Content!.Id}",
                result),
            ResponseType.VALIDATION_ERROR => BadRequest(result),
            ResponseType.INTERNAL_ERROR => new ObjectResult(result)
                { StatusCode = (int)HttpStatusCode.InternalServerError },
            _ => new ObjectResult(result)
                { StatusCode = (int)HttpStatusCode.InternalServerError }
        };
    }

    [HttpPut("{id}/in-progress")]
    public async Task<IActionResult> SetTaskInProgress(string id)
    {
        var result = await setTaskInProgressUseCase
            .SetTaskInProgress(id);

        return result.Type switch
        {
            ResponseType.VALIDATION_ERROR => BadRequest(result),
            ResponseType.SUCCESS => NoContent(),
            ResponseType.INTERNAL_ERROR => new ObjectResult(result)
                { StatusCode = (int)HttpStatusCode.InternalServerError },
            _ => new ObjectResult(result)
                { StatusCode = (int)HttpStatusCode.InternalServerError }
        };
    }
}
