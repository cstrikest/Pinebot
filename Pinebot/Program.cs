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


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("正在初始化机器人...");
            WebSocket server;
            try
            {
                server = new WebSocket("ws://127.0.0.1.s", 8182, LogLevel.Debug);
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