using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Program
{
	public static void Main(string[] args)
	{
		if (args.Length == 2)
		{
			string target = args[0];
			string destination = args[1];
			if (File.Exists(target))
			{
				if (!File.Exists(destination))
				{
					File.Copy(target, destination);
				}
				else
				{
					error("Error: destination file \"" +  destination + "\" already exists.");
				}
			}
			else
			{
				error("Error: could not find source file \"" + target + "\"");
			}
		}
		else
		{
			error("Error: cp expecting 2 arguments, found " + args.Length.ToString());
		}
	}
	public static void error(string message)
	{
		ConsoleColor pre = Console.ForegroundColor;
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine(message);
		Console.ForegroundColor = pre;
	}
}