using Bd.Icm.Core;

namespace Bd.Icm.Web.Dto
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string NewPassword { get; set; }
        public bool RemovePassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsDisabled { get; set; }
        public RoleType[] Roles { get; set; }
        public bool IsNew { get; set; }
        public User Modifier { get; set; }
    }
}
