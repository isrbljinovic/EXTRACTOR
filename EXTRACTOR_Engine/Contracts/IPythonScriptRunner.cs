using EXTRACTOR_Engine.Enums;

namespace EXTRACTOR_Engine.Contracts
{
    public interface IPythonScriptRunner
    {
        bool RunScript(string pythonPath, string args, Actions action);
    }
}