namespace PineBot.Function;

interface IAddin
{
    string? OnMessage();
    Func<string?, object?, EventArgs> GetResponseFunction();
}

public class Addin
{
    public string Name { get; internal set; }
    public string Version { get; internal set; }
    
}