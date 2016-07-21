using System.Collections.Generic;

namespace Bd.Icm.Web.Dto
{
    public class Instrument
    {
        public int InstrumentId { get; set; }
        public string Type { get; set; }
        public int MajorRevision { get; set; }
        public int MinorRevision { get; set; }
        public InstrumentType SapPartType { get; set; }
        public string NickName { get; set; }
        public string SerialNumber { get; set; }
        public bool IsNew { get; set; }
        public List<Part> Parts { get; set; }

        public Instrument()
        {
            Parts = new List<Part>();
        }
    }
}
