using System;
using System.Diagnostics;
using EXTRACTOR_Engine.Contracts;
using EXTRACTOR_Engine.Enums;

namespace EXTRACTOR_Engine.Services
{
    public class PythonScriptRunner : IPythonScriptRunner
    {
        public bool RunScript(string pythonPath, string args, Actions action)
        {
            string result = string.Empty;
            string error = string.Empty;
            string command = $"{ActionsExtensions.GetString(action)} {args}";

            try
            {
                var info = new ProcessStartInfo(pythonPath);
                info.Arguments = command;

                info.RedirectStandardInput = false;
                info.RedirectStandardOutput = true;
                info.RedirectStandardError = true;
                info.UseShellExecute = false;
                info.CreateNoWindow = true;

                using (var proc = new Process())
                {
                    proc.StartInfo = info;
                    proc.Start();
                    proc.WaitForExit();
                    if (proc.ExitCode == 0)
                    {
                        result = proc.StandardOutput.ReadToEnd();
                        return true;
                    }
                    else
                    {
                        error = proc.StandardError.ReadToEnd();
                        result = proc.StandardOutput.ReadToEnd();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("R Script failed:");
                Debug.WriteLine(result);
                Debug.WriteLine(ex);
                return false;
            }
        }
    }
}