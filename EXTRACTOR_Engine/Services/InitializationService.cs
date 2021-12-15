using System;
using System.Collections.Generic;
using System.Text;
using EXTRACTOR_Engine.Contracts;

namespace EXTRACTOR_Engine.Services
{
    public class InitializationService : IInitializationService
    {
        public bool Initialize()
        {
            throw new NotImplementedException();
        }

        private string GetPythonPath()
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