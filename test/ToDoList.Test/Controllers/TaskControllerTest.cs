using ToDoList.Api.Controllers;
using ToDoList.Application.UseCases.Interfaces;

namespace ToDoList.Test.Controllers;

public partial class TaskControllerTest
{
    private readonly TaskController _taskController;
    private readonly Mock<IGetTasksUseCase> _getTasksUseCase;
    private readonly Mock<ICreateTaskUseCase> _createTaskUseCase;
    private readonly Mock<ISetTaskInProgressUseCase> _setTaskInProgressUseCase;
    private readonly Mock<ISetTaskDoneUseCase> _setTaskDoneUseCase;
    private readonly Mock<ISetTaskCanceledUseCase> _setTaskCanceledUseCase;

    public TaskControllerTest()
    {
        _getTasksUseCase = new();
        _createTaskUseCase = new();
        _setTaskInProgressUseCase = new();
        _setTaskDoneUseCase = new();
        _setTaskCanceledUseCase = new();
        _taskController = new(
            _getTasksUseCase.Object,
            _createTaskUseCase.Object,
            _setTaskInProgressUseCase.Object,
            _setTaskDoneUseCase.Object,
            _setTaskCanceledUseCase.Object);
    }
}
