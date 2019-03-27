using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace touch
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                if (!File.Exists(args[0]))
                {
                    File.Create(args[0]);
                }
                else
                {
                    ConsoleColor pre = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: file already exists.");
                    Console.ForegroundColor = pre;
                }
            }
            else
            {
                ConsoleColor pre = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: touch expects 1 argument, found 0.");
                Console.ForegroundColor = pre;
            }
        }
    }
}
