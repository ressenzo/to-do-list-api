using System.Net;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Requests;
using ToDoList.Application.Responses;
using ToDoList.Application.UseCases.Interfaces;

namespace ToDoList.Presentation.Controllers;

[ApiController]
[Route("tasks")]
public class TaskController(
    ICreateTaskUseCase createTaskUseCase) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTask(
        CreateTaskRequest request)
    {
        var result = await createTaskUseCase
            .CreateTask(request.Description);
        
        return result.Type switch
        {
            ResponseType.SUCCESS => Ok(result),
            ResponseType.VALIDATION_ERROR => BadRequest(result),
            ResponseType.INTERNAL_ERROR => new ObjectResult(result)
                { StatusCode = (int)HttpStatusCode.InternalServerError },
            _ => NoContent()
        };
    }
}
