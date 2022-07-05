using Fleck;
using PineBot.Message;
using Newtonsoft.Json.Linq;

namespace PineBot.Handler;

public class WebSocketHandler
{
    private WebSocketServer server;
    public event EventHandler<EventMessage> NewMessage;
    public bool isConnected;
    public IWebSocketConnection? Conn;
    public ulong RecievedData { get; set; }

    public WebSocketHandler(string ipAddress, int port, LogLevel debugLevel)
    {
        FleckLog.Level = debugLevel;
        server = new WebSocketServer(ipAddress + ":" + port.ToString());
        server.Start(socket =>
        {
            socket.OnOpen = () =>
            {
                Console.WriteLine("启动Websocket服务。");
                Conn = socket;
                isConnected = true;
            };
            socket.OnClose = () =>
            {
                Console.WriteLine("关闭Websocket服务。");
                Conn = null;
                isConnected = false;
            };
            socket.OnMessage = message =>
            {
                Console.WriteLine(message);
                try
                {
                    NewMessage?.Invoke(this, new EventMessage(JObject.Parse(message)));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Json处理异常。");
                }
            };
        });
    }
}