using Bd.Icm.Core;

namespace Bd.Icm
{
    public interface IPart
    {
        string MfgPartNumber { get; }
        string Description { get; }
        int? InstrumentId { get; }
        int? ParentPartId { get; }
        string DateCode { get; }
        string LotCode { get; }
        string SapPartNumber { get; }
        int RevisionNumber { get; }
        int? DashNumber { get; }
        string DocumentNumber { get; }
        string SerialNumber { get; }
        PartType SapPartType { get; }
        string Name { get; }
        int Id { get; }
    }
}