using ToDoList.Application.UseCases.Interfaces;
using ToDoList.Presentation.Controllers;

namespace ToDoList.Test.Controllers;

public partial class TaskControllerTest
{
    private readonly TaskController _taskController;
    private readonly Mock<ICreateTaskUseCase> _createTaskUseCase;

    public TaskControllerTest()
    {
        _createTaskUseCase = new();
        _taskController = new(
            _createTaskUseCase.Object);
    }
}
