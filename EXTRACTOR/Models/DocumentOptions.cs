using System.Collections.Generic;
using EXTRACTOR_Engine.Enums;

namespace EXTRACTOR.Models
{
    public class DocumentOptions
    {
        public string Name { get; set; }
        public string DocumentPath { get; set; }
        public List<int> TablesToConvert { get; set; } = new List<int>();
        public Actions Conversion { get; set; } = Actions.ToOneFile;
    }
}