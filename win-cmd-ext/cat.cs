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
		if (args.Length > 0)
		{
			ConsoleColor t = mapColor(args[args.Length - 1]);
			bool colorfound = t != Console.ForegroundColor;
			if (colorfound && args.Length == 1)
			{
				error("Error: no file specified.");
			}
			int num = colorfound ? args.Length - 1:args.Length;
			for (int i = 0; i < num; i++)
			{
				if (File.Exists(args[i]))
				{
					printing(args[i]);
					string[] stuff = File.ReadAllLines(args[i]);
					ConsoleColor pre = Console.ForegroundColor;
					Console.ForegroundColor = t;
					foreach (string g in stuff)
					{
						Console.WriteLine(g);
					}
					Console.ForegroundColor = pre;
				}
				else
				{
					error("Could not find file " + args[i] + ".");
				}
			}
		}
		else
		{
			error("Error: no file specified.");
		}
	}
	public static void error(string message)
	{
		ConsoleColor pre = Console.ForegroundColor;
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine(message);
		Console.ForegroundColor = pre;
	}
	public static void printing(string message)
	{
		Console.Write("Printing ");
		ConsoleColor pre = Console.ForegroundColor;
		Console.ForegroundColor = ConsoleColor.Green;
		Console.Write(message);
		Console.ForegroundColor = pre;
		Console.WriteLine(":");
	}
	private static ConsoleColor mapColor(string argument)
	{
		for (int i = 0; i < 16; i++)
		{
			ConsoleColor curCol = (ConsoleColor)i;
			string colstr = curCol.ToString().ToLower();
			if (argument.ToLower() == colstr)
			{
				return curCol;
			}
		}
		return Console.ForegroundColor;
	}
}