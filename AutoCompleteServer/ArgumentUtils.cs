using System;
using System.IO;

namespace AutoCompleteServer
{
    public static class ArgumentUtils
    {
        public static bool ValidateArgument(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Неправильное количество аргументов");
                return false;
            }
            int port;
            if (int.TryParse(args[1], out port))
            {
                Console.WriteLine("Неправильное количество аргументов");
                return false;
            }

            if (File.Exists(args[0]))
            {
                Console.WriteLine("Файл словаря отсутствует");
                return false;
            }
            return true;
        }
    }
}