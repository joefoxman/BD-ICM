using System;

namespace Bd.Icm
{
    public interface IDataObject
    {
        int CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        byte[] RowVersion { get; }
        int ModifiedBy { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}
