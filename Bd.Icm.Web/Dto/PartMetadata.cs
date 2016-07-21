using System;

namespace Bd.Icm.Web.Dto
{
    public class PartMetadata
    {
        public int Id { get; set; }
        public string MetaKey { get; set; }
        public string MetaValue { get; set; }
        public bool IsNew { get; set; }
        public DateTime ModifiedDate { get; set; }
        public User Modifier { get; set; }
    }
}
