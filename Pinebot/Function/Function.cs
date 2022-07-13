using PineBot.Message;



namespace PineBot.Function;

public interface IAddin
{
    public void OnMessage(object? sender, MessageEventMessage e);
    public EventHandler<MessageEventMessage> GetResponseFunction();
}

public class Addin
{
    public string Name { get; internal set; }
    public string Version { get; internal set; }
    
}