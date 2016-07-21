namespace Bd.Icm.Web.Dto
{
    public class PartNode
    {
        public int PartId { get; set; }
        public int? ParentPartId { get; set; }
        public int? InstrumentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
    }
}
