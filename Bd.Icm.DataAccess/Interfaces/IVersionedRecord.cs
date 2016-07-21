using Bd.Icm.Core;

namespace Bd.Icm.DataAccess.Interfaces
{
    public interface IVersionedRecord
    {
        int Id { get; set; }
        int EffectiveFrom { get; set; }
        int EffectiveTo { get; set; }
        int RowVersion { get; set; }
        int ModificationType { get; set; }
    }
}
