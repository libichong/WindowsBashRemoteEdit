using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BashMate
{
    class Program
    {
        public static Process RunCommand(string fileName, string arguments, string rootPath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(fileName, arguments);

            //Set the working directory for the process
            startInfo.WorkingDirectory = rootPath;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = false;
            startInfo.RedirectStandardError = false;

            //Create a new process based on the startinfo
            Process process = new Process();
            process.StartInfo = startInfo;

            //Start the process and return the process object
            process.Start();

            return process;
        }

        private static string strBashMate = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "BashMate.txt");

        static void Main(string[] args)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = Path.GetDirectoryName(strBashMate);
            watcher.Filter = Path.GetFileName(strBashMate);
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.EnableRaisingEvents = true;
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);

            Console.WriteLine("Listening...");
            for(;;)
            {
            }
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                string content = File.ReadAllText(strBashMate).Trim();
                string path = "", args = "";
                int start = content.IndexOf('@'), end = content.LastIndexOf('@');
                path = content.Substring(start + 6, end - 6 - start);
                path = string.Format("{0}:{1}", path[0], path.Substring(1)).Replace('/', '\\');
                content = content.Substring(0, start) + content.Substring(end + 1);
                string cmd = content.Substring(0, content.IndexOf(' '));
                args = content.Substring(content.IndexOf(' ') + 1);
                RunCommand(cmd, args, path);
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
