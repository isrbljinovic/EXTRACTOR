using System.Collections.Generic;

namespace EXTRACTOR.Models
{
    public class DocumentOptions
    {
        public string Name { get; set; }
        public string DocumentPath { get; set; }
        public string Tables { get; set; } = string.Empty;
        public string Conversion { get; set; }

        public List<string> Conversions { get; set; } = new List<string>
        {
            "Prebaci u CSV",
            "Prebaci u jednu Excel datoteku",
            "Prebaci u različite Excel datoteke"
            //,"Generiraj SQL"
        };
    }
}