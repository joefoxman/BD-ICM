namespace Bd.Icm.DataAccess.Dto
{
    public class PartSearchResult
    {
        public int PartId { get; set; }
        public int? ParentPartId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DocumentNumber { get; set; }
        public string SerialNumber { get; set; }
        public string SapPartNumber { get; set; }
        public int Level { get; set; }
    }
}
