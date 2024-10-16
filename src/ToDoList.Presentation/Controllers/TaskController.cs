using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Presentation.Controllers;

[ApiController]
[Route("tasks")]
public class TaskController() : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}
