using System;
using System.Threading.Tasks;

namespace AutoCompleteServer
{
    class Program
    {
        static void Main(string[] args)
        {
            if(!ArgumentUtils.ValidateArgument(args))
                return;

            CompleteUtils.DictionaryPath = args[0];
            int port = Int16.Parse(args[1]);

            var server = new ServerProcessor(port);
            var listen = new Task(() => server.Start());
            listen.Start();
            Console.WriteLine("Для остановки сервера нажмите ENTER...");
            Console.ReadLine();
        }
    }
}