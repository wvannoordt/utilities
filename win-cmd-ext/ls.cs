using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace ls
{
    class Program
    {
        static ConsoleColor RED = ConsoleColor.Red;
        static ConsoleColor pre;
        static bool verbose = false;
        static int minimum_space_buffer = 4;
        static void Main(string[] args)
        {
            try
            {
                pre = Console.ForegroundColor;
                string target = @".\";
                verbose = args.Contains("-l");
                if (args.Length > 0)
                {
                    if (verbose && args.Length == 2)
                    {
                        target = args[1];
                    }
                    else if (!verbose)
                    {
                        target = args[0];
                    }
                }
                if (args.Length > 1 && !verbose)
                {
                    wtln("ls: too many arguments.", RED);
                }
                else
                {
                    string[] dirs = Directory.GetDirectories(target);
                    string[] files = Directory.GetFiles(target);
                    if (dirs.Length + files.Length != 0)
                    {
                        ListEntry[] entries = buildentrylist(dirs, files);
                        if (verbose) { writeVerbose(entries); }
                        else { writeConcise(entries); }
                    }
                }
                Console.ForegroundColor = pre;
            }
            catch
            {
                wtln("Error: cannot find specified directory.", RED);
            }
        }
        private static void writeVerbose(ListEntry[] entries)
        {
            for (int i = 0; i < entries.Length; i++)
            {
                entries[i].Write();
                Console.WriteLine();
            }
        }
        private static void writeConcise(ListEntry[] entries)
        {
            int window_width = Console.BufferWidth;
            int max = int.MinValue;
            int n = entries.Length;
            for (int i = 0; i < n; i++)
            {
                if (entries[i].Footprint > max) { max = entries[i].Footprint; }
            }
            int binsize = max + minimum_space_buffer;
            int line_bin_count = window_width / binsize;
            for (int i = 0; i < n; i++)
            {
                entries[i].Write(binsize);
                if ((i+1) % line_bin_count == 0) { Console.WriteLine(); }
            }
        }
        private static ListEntry[] buildentrylist(string[] dirs, string[] files)
        {
            ListEntry[] allentries = new ListEntry[dirs.Length + files.Length];
            for (int i = 0; i < dirs.Length; i++)
            {
                allentries[i] = new ListEntry(dirs[i], true);
            }
            for (int i = 0; i < files.Length; i++)
            {
                allentries[i+dirs.Length] = new ListEntry(files[i], false);
            }
            Quicksort(allentries, 0, allentries.Length - 1);
            return allentries;
        }
        public static void Quicksort(ListEntry[] elements, int left, int right)
        {
            int i = left, j = right;
            ListEntry pivot = elements[(left + right) / 2];

            while (i <= j)
            {
                while (elements[i] < pivot)
                {
                    i++;
                }

                while (elements[j] > pivot)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    ListEntry tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (left < j)
            {
                Quicksort(elements, left, j);
            }

            if (i < right)
            {
                Quicksort(elements, i, right);
            }
        }
        private static void wtln(string msg, ConsoleColor col)
        {
            setcol(col);
            Console.WriteLine(msg);
            setcol(pre);
        }
        private static void setcol(ConsoleColor C)
        {
            Console.ForegroundColor = C;
        }
    }
    public class ListEntry
    {
		string[] cmd_paths;
		string runpath;
        string displayname;
        bool isDir, isCmd, isExe;
        FileAttributes attributes;
        DirectoryInfo info;
        const ConsoleColor fileColor = ConsoleColor.White;
        const ConsoleColor dirColor = ConsoleColor.Cyan;
		const ConsoleColor cmdPathColor = ConsoleColor.Blue;
		const ConsoleColor exeColor = ConsoleColor.Magenta;
        public int Footprint
        {
            get { return displayname.Length; }
        }
		private string GetApplicationPath()
		{
			var location = new Uri(System.Reflection.Assembly.GetEntryAssembly().GetName().CodeBase); 
			return new FileInfo(location.AbsolutePath).Directory.FullName; 
		}
        public ListEntry(string path, bool isdir)
        {
            displayname = path.Split('\\').Last();
            isDir = isdir;
			cmd_paths = Environment.GetEnvironmentVariable("Path").ToString().Split(';');
			runpath = GetApplicationPath();
			runpath = System.IO.Path.GetDirectoryName(runpath);
			isCmd = check_cmd(displayname);
			isExe = endsWithExe(displayname);
        }
		private bool endsWithExe(string s)
		{
			bool[] exe =
			{
				s.EndsWith(".exe"),
				s.EndsWith(".bat"),
				s.EndsWith(".lnk"),
				s.EndsWith(".rdp")
			};
			return validateall(exe);
		}
		private bool validateall(bool[] s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i]) {return true;}
			}
			return false;
		}
		private bool check_cmd(string disp)
		{
			string abs = runpath + "\\" + disp;
			return cmd_paths.Contains(abs);
		}
        public void Write()
        {
            Write(0);
        }
        public void Write(int buffer)
        {
            Console.ForegroundColor = fileColor;
			if (isCmd) {Console.ForegroundColor = cmdPathColor;}
            else if (isDir) { Console.ForegroundColor = dirColor; }
			else if (isExe) {Console.ForegroundColor = exeColor;}
            Console.Write(bufferstring(displayname, buffer));
        }
        private string bufferstring(string t, int n)
        {
            string output = t;
            while (output.Length < n)
            {
                output += ' ';
            }
            return output;
        }
        public static bool operator >(ListEntry A, ListEntry B)
        {
            string a = A.displayname.ToLower();
            string b = B.displayname.ToLower();
            int maxindex = Math.Min(a.Length, b.Length);
            for (int i = 0; i < maxindex; i++)
            {
                if (a[i] != b[i])
                {
                    return (int)a[i] > (int)b[i];
                }
            }
            return false;
        }
        public static bool operator <(ListEntry A, ListEntry B)
        {
            string a = A.displayname.ToLower();
            string b = B.displayname.ToLower();
            int maxindex = Math.Min(a.Length, b.Length);
            for (int i = 0; i < maxindex; i++)
            {
                if (a[i] != b[i])
                {
                    return (int)a[i] < (int)b[i];
                }
            }
            return false;
        }
    }
}
