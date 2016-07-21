using System;
using Bd.Icm.Core;

namespace Bd.Icm.Web.Dto
{
    public class PartChange
    {
        public int PartId { get; set; }
        public int? ParentPartId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Modifier { get; set; }
        public ModificationType ModificationType { get; set; }
        public int EffectiveFrom { get; set; }
        public int EffectiveTo { get; set; }
    }
}
