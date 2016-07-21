using System;

namespace Bd.Icm.DataAccess.Interfaces
{
    public interface IAuditedRecord
    {
        int CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        int ModifiedBy { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}
