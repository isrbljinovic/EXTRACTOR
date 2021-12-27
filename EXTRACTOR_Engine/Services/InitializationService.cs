using System;
using System.Diagnostics;
using EXTRACTOR_Engine.Contracts;

namespace EXTRACTOR_Engine.Services
{
    public class InitializationService : IInitializationService
    {
        public bool Initialize()
        {
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + "pipsetup.bat");
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            // *** Redirect the output ***
            //processInfo.RedirectStandardError = true;
            //processInfo.RedirectStandardOutput = true;

            process = Process.Start(processInfo);
            process.WaitForExit();
            process.Close();
            return true;
        }

        public string GetPythonPath()
        {
            string[] entries = Environment.GetEnvironmentVariable("path").Split(';');
            string pyPath = "";

            foreach (string entry in entries)
            {
                if (entry.ToLower().Contains("python"))
                {
                    var directories = entry.Split('\\');
                    foreach (string directorie in directories)
                    {
                        if (!directorie.ToLower().Contains("python"))
                        {
                            pyPath += directorie + '\\';
                            continue;
                        }
                        else
                        {
                            pyPath += directorie + "\\python.exe";
                            return pyPath;
                        }
                    }
                }
            }
            throw new ApplicationException("Python not found");
        }
    }
}