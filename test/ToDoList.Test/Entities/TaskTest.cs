using ToDoList.Domain.Enums;
using ToDoList.Test.Builders;
using ToDoList.Test.Commons;
using Task = ToDoList.Domain.Entities.Task;

namespace ToDoList.Test.Entities;

public class TaskTest
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Description_WhenIsInvalidValue_ShouldBeFalse(
        string description)
    {
        // Arrange - Act
        var task = Task.Construct(description);

        // Assert
        task.IsValid().ShouldBeFalse();
        task.Errors.ShouldNotBeEmpty();
        task.Errors.Count().ShouldBe(1);
    }

    [Fact]
    public void Description_WhenLengthIsGreaterThan100_ShouldBeFalse()
    {
        // Arrange
        var description = Helper.GenerateRandomString(
            length: 101);

        // Act
        var task = Task.Construct(description);

        // Assert
        task.IsValid().ShouldBeFalse();
        task.Errors.ShouldNotBeEmpty();
        task.Errors.Count().ShouldBe(1);
    }

    [Fact]
    public void Status_WhenSetAsDone_ShouldBeStatusDone()
    {
        // Arrange
        var task = new TaskBuilder()
            .Build();

        // Act
        task.SetAsDone();

        // Assert
        task.Status.ShouldBe(Status.DONE);
    }

    [Fact]
    public void Status_WhenSetAsCanceled_ShouldBeStatusCanceled()
    {
        // Arrange
        var task = new TaskBuilder()
            .Build();

        // Act
        task.SetAsCanceled();

        // Assert
        task.Status.ShouldBe(Status.CANCELED);
    }

    [Fact]
    public void Status_WhenSetAsInProgress_ShouldBeStatusInProgress()
    {
        // Arrange
        var task = new TaskBuilder()
            .Build();

        // Act
        task.SetAsInProgress();

        // Assert
        task.Status.ShouldBe(Status.IN_PROGRESS);
    }

    [Theory]
    [InlineData(Status.DONE)]
    [InlineData(Status.CANCELED)]
    public void Status_WhenSetAsInProgressButStatusIsDoneOrCanceled_ShouldNotChangeStatus(
        Status status)
    {
        // Arrange
        var task = new TaskBuilder()
            .WithStatus(status)
            .Build();

        // Act
        task.SetAsInProgress();

        // Assert
        task.Status.ShouldBe(status);
    }

    [Theory]
    [InlineData(Status.CREATED)]
    [InlineData(Status.CANCELED)]
    public void Status_WhenSetAsDoneButStatusIsCreatedOrCanceled_ShouldNotChangeStatus(
        Status status)
    {
        // Arrange
        var task = new TaskBuilder()
            .WithStatus(status)
            .Build();

        // Act
        task.SetAsDone();

        // Assert
        task.Status.ShouldBe(status);
    }

    [Fact]
    public void Status_WhenSetAsCanceledButStatusIsDone_ShouldNotChangeStatus()
    {
        // Arrange
        var task = new TaskBuilder()
            .WithStatus(Status.DONE)
            .Build();

        // Act
        task.SetAsCanceled();

        // Assert
        task.Status.ShouldBe(Status.DONE);
    }

    [Fact]
    public void Task_WhenCreateObject_ShouldSetValuesAndStatusShouldBeCreatedAndCreationDateShouldBeNow()
    {
        // Arrange
        var description = "My task";

        // Act
        var task = Task.Construct(description);

        // Assert
        task.IsValid().ShouldBeTrue();
        task.Description.ShouldBe(description);
        task.Status.ShouldBe(Status.CREATED);
        task.CreationDate.ShouldBeLessThan(DateTime.Now);
    }
}
