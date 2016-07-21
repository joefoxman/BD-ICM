namespace Bd.Icm.Web
{
    public class AppUserState
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return $"{UserId}|{UserName}|{FirstName}|{LastName}";
        }

        public bool FromString(string userState)
        {
            if (string.IsNullOrWhiteSpace(userState))
                return false;

            var parts = userState.Split('|');
            if (parts.Length < 4)
                return false;

            UserId = int.Parse(parts[0]);
            UserName = parts[1];
            FirstName = parts[2];
            LastName = parts[3];
            return true;
        }

        public void FromUser(User user)
        {
            UserId = user.Id;
            UserName = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }
    }
}
