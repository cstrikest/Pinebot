using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PineBot.Message;

namespace PineBot.Handler;

public class MessageHandler
{
    public event EventHandler<MessageEventMessage> MessageEventHandler;
    public event EventHandler<MetaEventMessage> MetaEventHandler; 
    public void OnMessage(object? sender, EventMessage e)
    {
        switch ((string?)e.J["post_type"])
        {
            case "message":
                MessageEventHandler.Invoke(this, new MessageEventMessage(e.J));
                break;
            case "request":
                break;
            case "notice":
                break;
            case "meta_event":
                var meM = new MetaEventMessage(e.J);
                break;
        }
        /*
{"post_type":"message","message_type":"group","time":1657008992,"self_id":3434696172,"sub_type":"normal","anonymous":null,"message_seq":1154886,"raw_message":"晋哥哥刀削面","
font":0,"group_id":705124696,"message":"晋哥哥刀削面","message_type":"group","sender":{"age":0,"area":"","card":"景德镇员工","level":"","nickname":"想赢的手帐","role":"member
","sex":"unknown","title":"","user_id":2391272987},"user_id":2391272987,"message_id":-689777213}
    
*/
    }
}