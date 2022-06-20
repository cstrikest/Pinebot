using Fleck;

namespace PineBot
{
    class WebSocket
    {
        private WebSocketServer server;
        public List<IWebSocketConnection> allSockets = new();

        public WebSocket(string ipAddress, int port, LogLevel debugLevel)
        {
            FleckLog.Level = debugLevel;
            server = new WebSocketServer(ipAddress + ":" + port.ToString());
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("启动Websocket服务。");
                    allSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    Console.WriteLine("关闭Websocket服务。");
                    allSockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    Console.WriteLine(message);
                    // allSockets.ToList().ForEach(s => s.Send("Echo: " + message));
                    socket.Send(message);
                };
            });
        }
    }


    interface IServerConfig
    {
        Action open { get; set; }
        Action close { get; set; }
        Action<string> msg { get; set; }

    }
    class Server
    {
        private string ipAddress { get; }
        public Server(string ipStr)
        {
            ipAddress = ipStr;
        }
        public void Start(Action<IServerConfig> config)
        {
            Console.WriteLine("Connecting to {0} ...", ipAddress);
            
        }
    }

    class Config : IServerConfig
    {
        public Action open { get; set; }
        public Action close { get; set; }

        public Action<string> msg { get; set; }
        
    }
    internal class Program
    {   
        static void Main(string[] args)
        {
            var s = new Server("192.168.0.1");
            s.Start(config =>
            {
                config.open = () =>
                {
                    Console.WriteLine("things while opening...");
                };
                config.close = (() =>
                {
                    Console.WriteLine("things while closing...");
                });
                config.msg = b =>
                {
                    Console.WriteLine(b);
                };
            });

            void Fucn(IServerConfig config)
            {
                config.close();
            }
            
            return;
            
            Console.WriteLine("正在初始化机器人...");
            WebSocket server;
            try
            {
                server = new WebSocket("ws://127.0.0.1", 8182, LogLevel.Debug);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            Console.WriteLine("网络连接初始化成功。");
            var input = Console.ReadLine();
            while (input != "exit")
            {
                foreach (var socket in server.allSockets.ToList())
                {
                    socket.Send(input);
                }

                input = Console.ReadLine();
            }
        }
    }
}