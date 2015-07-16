﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace skb_kontur_autocomplete
{
    class Program
    {
        static void Main(string[] args)
        {
            AutoComplete complete;
            if (args.Length > 0)
                complete = new AutoComplete(args[1]);
            else
                complete = new AutoComplete();

            while (true)
            {
                var input = Console.ReadLine();
                Console.WriteLine(complete.GetVariant(input));
            }
        }
    }
}
