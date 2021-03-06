using Fleck;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PineBot.Util;
using PineBot.Handler;
using PineBot.Message;
using PineBot.Function;
using PineBot.Function.Arena;

namespace PineBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("正在初始化机器人...");
            WebSocketHandler s;
            try
            {
                s = new WebSocketHandler("ws://127.0.0.1", 8182, LogLevel.Info);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            var handler = new MessageHandler();
            var addins = new List<IAddin>
            {
                new Arena()
            };
            handler.LoadFunctions(addins);
            s.ReceiveJsonEvent += handler.OnMessage;

            while (true)
            {
                Console.ReadKey();
            }
        }
    }
}