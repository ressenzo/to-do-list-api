namespace ToDoList.Infrastructure.Settings;

internal record DatabaseSettings(string ConnectionString,
    string Name,
    string Collection)
{
    public DatabaseSettings() :
        this(ConnectionString: null!,
            Name: null!,
            Collection: null!) { }    
}
