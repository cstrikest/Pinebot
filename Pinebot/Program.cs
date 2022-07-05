using Fleck;
using Newtonsoft.Json;

namespace PineBot
{
    class WebSocket
    {
        private WebSocketServer server;
        public event EventHandler<EventMessage> NewMessage;
        public bool isConnected;
        public IWebSocketConnection? Conn;
        public ulong RecievedData { get; set; }

        public WebSocket(string ipAddress, int port, LogLevel debugLevel)
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
                    // Console.WriteLine(message);
                    NewMessage?.Invoke(this, new EventMessage(message));
                };
            });
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine(JsonConvert.SerializeObject(new 
            // {
            //     name = "sss",
            //     id = 2888
            // }));

            Console.WriteLine("正在初始化机器人...");
            WebSocket s;
            try
            {
                s = new WebSocket("ws://127.0.0.1", 8182, LogLevel.Debug);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            s.NewMessage += OnMessage;

            while (true)
            {
                while (s.isConnected)
                {
                    Console.WriteLine("WebSocket连接成功。机器人已启动。");
                    Console.ReadKey();
                }
            }
        }

        static void OnMessage(object? sender, EventMessage e)
        {
            var m = JsonConvert.DeserializeObject<EventMessage>(e.JS);
            if (m.post_type == "meta_event")
            {
                var me = JsonConvert.DeserializeObject<MetaEventMessage>(e.JS);
                if (me.meta_event_type == "heartbeat")
                {
                    DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
                    DateTime dt = startTime.AddSeconds(me.time);
                    Console.WriteLine("heartbeat." + dt.ToString("yyyy/MM/dd HH:mm:ss"));
                }
            }
        }
    }
}