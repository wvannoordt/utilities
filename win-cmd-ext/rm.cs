using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace rm
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                try
                {
                    bool doRec = args[0] == "-r";
                    int baseindex = 0;
                    if (doRec)
                    {
                        baseindex = 1;
                        for (int i = baseindex; i < args.Length; i++)
                        {
                            if (!args[i].Contains("*"))
                            {
                                runDeleteDirectory(args[i]);
                            }
                            else
                            {
                                if (args[i].Contains("\\") || args[i].Contains(".."))
                                {
                                    er("Error: cannot recursively remove directories in relative path.");
                                }
                                else
                                {
                                    string[] filenames = Directory.GetDirectories(@".\");
                                    bool endswith, startswith, contains;
                                    string term = extract_term(args[i], out startswith, out endswith, out contains);
                                    for (int j = 0; j < filenames.Length; j++)
                                    {
                                        string candidate = filenames[j].Split('\\').Last();
                                        if (endswith && candidate.EndsWith(term))
                                        {
                                            runDeleteDirectory(candidate);
                                        }
                                        if (startswith && candidate.StartsWith(term))
                                        {
                                            runDeleteDirectory(candidate);
                                        }
                                        if (contains && candidate.Contains(term))
                                        {
                                            runDeleteDirectory(candidate);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = baseindex; i < args.Length; i++)
                        {
                                if (!args[i].Contains("*"))
                                {
                                    runDeleteFile(args[i]);
                                }
                                else
                                {
                                    if (args[i].Contains("\\") || args[i].Contains(".."))
                                    {
                                        er("Error: cannot recursively remove files in relative path.");
                                    }
                                    else
                                    {
                                        string[] filenames = Directory.GetFiles(@".\");
                                        bool endswith, startswith, contains;
                                        string term = extract_term(args[i], out startswith, out endswith, out contains);
                                        for (int j = 0; j < filenames.Length; j++)
                                        {
                                            string candidate = Path.GetFileName(filenames[j]);
                                            if (endswith && candidate.EndsWith(term))
                                            {
                                                runDeleteFile(candidate);
                                            }
                                            if (startswith && candidate.StartsWith(term))
                                            {
                                                runDeleteFile(candidate);
                                            }
                                            if (contains && candidate.Contains(term))
                                            {
                                                runDeleteFile(candidate);
                                            }
                                        }
                                    }
                                }
                        }
                    }
                }
                catch
                {
                    er("Error: cannot remove specified targets.");
                }
               
            }
            else
            {
                er("Error: rm expecting 1 argument, found 0.");
            }
        }
        private static void runDeleteFile(string target)
        {
            if (File.Exists(target))
            {
                File.Delete(target);
            }
        }
        private static void ClearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.IsReadOnly = false;
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                ClearFolder(di.FullName);
                di.Delete();
            }
        }
        private static void runDeleteDirectory(string target)
        {
            if (Directory.Exists(target))
            {
                ClearFolder(target);
                Directory.Delete(target);
            }
        }
        private static void er(string msg)
        {
            ConsoleColor pre = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = pre;
        }
        private static void wn(string msg)
        {
            ConsoleColor pre = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(msg);
            Console.ForegroundColor = pre;
        }
        private static string extract_term(string raw, out bool start, out bool ends, out bool contains)
        {
            start = false;
            ends = false;
            contains = false;
            string output = string.Empty;
            for (int i = 0; i < raw.Length; i++)
            {
                if ((raw[i] == '*' && i != 0) && (raw[i] == '*' && i != raw.Length - 1))
                {
                    throw new Exception("error: bad filter");
                }
                if (raw[i] != '*')
                {
                    output += raw[i];
                }
            }
            if (raw[0] == '*' && raw.Last() != '*')
            {
                start = false;
                contains = false;
                ends = true;
            }
            if (raw[0] == '*' && raw.Last() == '*')
            {
                start = false;
                contains = true;
                ends = false;
            }
            if (raw[0] != '*' && raw.Last() == '*')
            {
                start = true;
                contains = false;
                ends = false;
            }
            return output;
        }
    }
}
