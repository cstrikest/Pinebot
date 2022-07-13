namespace PineBot.Message;
using Newtonsoft.Json.Linq;

public class EventMessage
{
    public JObject J { get; internal set; }
    public int time { get; set; }
    public long self_id { get; set; }
    public string post_type { get; set; }

    public EventMessage(JObject J)
    {
        this.J = J;
    }
}

public class MetaEventMessage : EventMessage
{
    public string meta_event_type { get; }

    public MetaEventMessage(JObject J) : base(J)
    {
        this.J = J;
        meta_event_type = (string)J["meta_event_type"];
    }
}

public class MessageEventMessage : EventMessage
{
    public string SubType { get; }
    public long MessageId { get; }
    public long UserId{get;}
    public string Message { get; }
    public string rawMessage { get; }
    public int font { get; }
    public JObject? sender { get; }

    public MessageEventMessage(JObject J) : base(J)
    {
        this.J = J;
        SubType = (string)J["sub_type"];
        MessageId = (long)J["message_id"];
        UserId = (long)J["user_id"];
        Message= (string)J["message"];
        rawMessage = (string)J["raw_message"];
        font = (int)J["font"];
        sender = (JObject)J["sender"];
    } 
    
}