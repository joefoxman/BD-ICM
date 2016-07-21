using System;
using Bd.Icm.DataAccess.Interfaces;

namespace Bd.Icm.DataAccess.Dto
{
    public class PartChange :
            IVersionedRecord,
            IAuditedRecord
    {
        public int Id { get; set; }
        public int EffectiveFrom { get; set; }
        public int EffectiveTo { get; set; }
        public int RowVersion { get; set; }
        public int? ParentPartId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DocumentNumber { get; set; }
        public int DashNumber { get; set; }
        public string SerialNumber { get; set; }
        public string SapPartNumber { get; set; }
        public int CreatedBy { get; set; }
        public string Creator { get; set; }
        public string Modifier { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? InstrumentCommitId { get; set; }
        public int ModificationType { get; set; }
        public int Level { get; set; }
    }
}
