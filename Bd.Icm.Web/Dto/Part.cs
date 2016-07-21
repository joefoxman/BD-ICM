using System;
using System.Collections.Generic;
using Bd.Icm.Core;

namespace Bd.Icm.Web.Dto
{
    public class Part
    {
        public int Id { get; set; }
        public int? InstrumentId { get; set; }
        public int? ParentPartId { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public string DocumentNumber { get; set; }
        public int? DashNumber { get; set; }
        public int RevisionNumber { get; set; }
        public string SapPartNumber { get; set; }
        public string Manufacturer { get; set; }
        public string MfgPartNumber { get; set; }
        public string LotCode { get; set; }
        public string DateCode { get; set; }
        public DateTime? CommittedDate { get; set; }
        public bool IsNew { get; set; }
        public PartType SapPartType { get; set; }
        public List<Part> Parts { get; set; }
        public List<PartAction> Actions { get; set; } 
        public List<PartMetadata> Metadata { get; set; } 
        public int EffectiveFrom { get; set; }
        public int EffectiveTo { get; set; }
        public string Modifier { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ModificationType ModificationType { get; set; }
        public string Creator { get; set; }
        public DateTime CreatedDate { get; set; }

        public Part()
        {
            Parts = new List<Part>();
            Actions = new List<PartAction>();
            Metadata = new List<PartMetadata>();
        }
    }
}
