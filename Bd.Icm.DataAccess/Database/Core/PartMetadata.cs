using Bd.Icm.DataAccess.Interfaces;

namespace Bd.Icm.DataAccess.Database
{
    public partial class PartMetadata : 
        IVersionedRecord,
        IAuditedRecord,
        ICommittableRecord
    {
    }
}
