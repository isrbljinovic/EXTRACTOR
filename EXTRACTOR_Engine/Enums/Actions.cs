using System;

namespace EXTRACTOR_Engine.Enums
{
    public enum Actions
    {
        ToCSVs,
        ToOneFile,
        ToSeparateFiles,
        ToSQL
    }

    public static class ActionsExtensions
    {
        public static string GetString(this Actions action)
        {
            switch (action)
            {
                case Actions.ToCSVs:
                    return @"..\..\..\Scripts\TablesToCSVs.py";

                case Actions.ToOneFile:
                    return @"..\..\..\Scripts\TablesToOneFile.py";

                case Actions.ToSeparateFiles:
                    return @"..\..\..\Scripts\TablesToSeparateFiles.py";

                case Actions.ToSQL:
                    return @"..\..\..\Scripts\TableToSql.py";

                default:
                    throw new ArgumentException();
            }
        }

        public static Actions GetAction(string action)
        {
            switch (action)
            {
                case "Prebaci u CSV":
                    return Actions.ToCSVs;

                case "Prebaci u jednu Excel datoteku":
                    return Actions.ToOneFile;

                case "Prebaci u različite Excel datoteke":
                    return Actions.ToSeparateFiles;

                case "Generiraj SQL":
                    return Actions.ToSQL;

                default:
                    return Actions.ToOneFile;
            }
        }
    }
}