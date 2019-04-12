using System;
using System.Diagnostics;

public class Program
{
	public static void Main(string[] args)
	{
		string exe = @"C:\Program Files (x86)\Notepad++\notepad++.exe";
		if (args.Length > 0)
		{
			Process.Start(exe, args[0]);
		}
		else
		{
			Process.Start(exe);
		}
	}
}