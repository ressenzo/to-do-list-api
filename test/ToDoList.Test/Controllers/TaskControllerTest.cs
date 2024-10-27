using ToDoList.Api.Controllers;
using ToDoList.Application.UseCases.Interfaces;

namespace ToDoList.Test.Controllers;

public partial class TaskControllerTest
{
    private readonly TaskController _taskController;
    private readonly Mock<ICreateTaskUseCase> _createTaskUseCase;
    private readonly Mock<ISetTaskInProgressUseCase> _setTaskInProgressUseCase;

    public TaskControllerTest()
    {
        _createTaskUseCase = new();
        _setTaskInProgressUseCase = new();
        _taskController = new(
            _createTaskUseCase.Object,
            _setTaskInProgressUseCase.Object);
    }
}
