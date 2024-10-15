namespace ToDoList.Test.Commons;

public static class Helper
{
    public static string GenerateRandomString(int length)
    {
        var random = new Random();
        return new string(Enumerable.Repeat("A", length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
