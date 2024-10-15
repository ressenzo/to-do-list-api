namespace ToDoList.Test.Builders;

public abstract class BaseBuilder<T> where T : class
{
    public abstract T Build();
}
