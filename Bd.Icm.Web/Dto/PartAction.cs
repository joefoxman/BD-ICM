using System;

namespace Bd.Icm.Web.Dto
{
    public class PartAction
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public PartActionType Action { get; set; }
        public DateTime ActionDate { get; set; }
        public bool IsNew { get; set; }
        public DateTime ModifiedDate { get; set; }
        public User Modifier { get; set; }
    }
}
