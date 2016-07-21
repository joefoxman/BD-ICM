namespace Bd.Icm.DataAccess.Interfaces
{
    public interface IConcurrencyRecord
    {
        byte[] RowVersion { get; set; }
    }
}
