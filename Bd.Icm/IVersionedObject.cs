using System;
using Bd.Icm.Core;

namespace Bd.Icm
{
    public interface IVersionedObject
    {
        int CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        int RowVersion { get; }
        int ModifiedBy { get; set; }
        DateTime ModifiedDate { get; set; }
        int EffectiveFrom { get; set; }
        int EffectiveTo { get; set; }
        ModificationType ModificationType { get; set; }
    }
}
