using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace open
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                try
                {
                    Process.Start(args[0]);
                }
                catch
                {
                    ConsoleColor pre = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: cannot open " + args[0] + ".");
                    Console.ForegroundColor = pre;
                }
            }
            else
            {
                ConsoleColor pre = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: open expecting 1 argument, found 0.");
                Console.ForegroundColor = pre;
            }
        }
    }
}
