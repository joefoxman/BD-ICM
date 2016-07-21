using System;

namespace Bd.Icm.Web.Dto
{
    public class InstrumentCommit
    {
        public int Id { get; set; }
        public int InstrumentId { get; set; }
        public string Notes { get; set; }
        public int EffectiveTo { get; set; }
        public int Revision { get; set; }
        public bool IsNew { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Creator { get; set; }
    }
}
