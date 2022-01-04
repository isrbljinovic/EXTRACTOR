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
            string result = string.Empty;
            string error = string.Empty;

            processInfo = new ProcessStartInfo("cmd.exe", "/c" + "pip install tabula");
            processInfo.CreateNoWindow = false;
            processInfo.UseShellExecute = false;
            // *** Redirect the output ***
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            process = Process.Start(processInfo);
            process.WaitForExit();
            if (process.ExitCode == 0)
            {
                result = process.StandardOutput.ReadToEnd();
                return true;
            }
            else
            {
                error = process.StandardError.ReadToEnd();
                result = process.StandardOutput.ReadToEnd();
                return false;
            }
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