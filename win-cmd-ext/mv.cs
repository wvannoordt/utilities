using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace mv
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 2)
                {
                    if (File.Exists(args[0]))
                    {
                        File.Move(args[0], args[1]);
                    }
                    else if (Directory.Exists(args[0]))
                    {
                        Directory.Move(args[0], args[1]);
                    }
                }
                else
                {
                    ConsoleColor pre = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: mv expecting 2 arguments, found " + args.Length.ToString());
                    Console.ForegroundColor = pre;
                }
            }
            catch
            {
                ConsoleColor pre = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: cannot find specified file.");
                Console.ForegroundColor = pre;
            }
        }
    }
}
