namespace PineBot;

using Newtonsoft.Json;

enum PostTypes
{
    message,
    request,
    notice,
    meta_event
}

class MessageChain
{
    public MessageChain()
    {
        
    }
}

class EventMessage
{
    public string JS { get; internal set; }
    public int time { get; set; }
    public long self_id { get; set; }
    public string post_type { get; set; }
    public EventMessage(string js)
    {
        JS = js;
    }
}

class MetaEventMessage : EventMessage
{
    public string meta_event_type { get; set; }
    
    public MetaEventMessage(string js) : base(js)
    {
        JS = js;
    }
}

// class message : EventMessage
// {
//     public string SubType { get; }
//     public int MessageId { get; }
//     public int UserId{get;}
//     public MessageChain Message { get; }
//     public string rawMessage { get; }
//     public int font { get; }
//     public object sender { get; }
//
//     // public message(object m) : base(m.json)
//     // {
//     //     
//     // }
//     
// }