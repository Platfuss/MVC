namespace MVC.Models.DbContexts;

public class LogItem(string source, string input, string result)
{
    public int Id { get; set; }
    public string Source { get; set; } = source;
    public string Input { get; set; } = input;
    public string Result { get; set; } = result;
}
