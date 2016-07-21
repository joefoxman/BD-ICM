using System;
using Bd.Icm.Core;

namespace Bd.Icm.Web.Dto
{
    public class UncommittedChange
    {
        public RecordType RecordType { get; set; }
        public int Id { get; set; }
        public int EffectiveFrom { get; set; }
        public int EffectiveTo { get; set; }
        public string Location { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Modifier { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Creator { get; set; }
        public ModificationType ModificationType { get; set; }
    }
}
