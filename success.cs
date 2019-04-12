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
		ConsoleColor pre = Console.ForegroundColor;
		Console.ForegroundColor = ConsoleColor.Yellow;
		string[] stuff = {
			".d8888b 888  888 .d8888b .d8888b .d88b. .d8888b .d8888b ",
			"88K     888  888d88P\"   d88P\"   d8P  Y8b88K     88K      ",
			"\"Y8888b.888  888888     888     88888888\"Y8888b.\"Y8888b. ",
			"    X88Y88b  888Y88b.   Y88b.   Y8b.         X88     X88 ",
			" 88888P' \"Y88888 \"Y8888P \"Y8888P \"Y8888  88888P' 88888P' "
		};
		foreach (string i in stuff)
		{
			Console.WriteLine(i);
		}
		Console.ForegroundColor = pre;
	}
}