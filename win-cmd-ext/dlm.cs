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
		int width = Console.BufferWidth;
		string delimitstring = args.Length > 0 ? args[0] : "#";
		ConsoleColor delimitcolor = args.Length > 1 ? mapColor(args[1]) : Console.ForegroundColor;
		ConsoleColor pre = Console.ForegroundColor;
		Console.ForegroundColor = delimitcolor;
		int n = delimitstring.Length;
		for (int i = 0; i < width; i++)
		{
			Console.Write(delimitstring[i%n]);
		}
		Console.ForegroundColor = pre;
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
