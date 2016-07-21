using System.Threading;
using Csla;
using Csla.Security;
using System;
using System.Security.Principal;

namespace Bd.Icm
{
    [Serializable]

    public class BdPrincipal : CslaPrincipal
    {
        private BdPrincipal(IIdentity identity)
            : base(identity)
        { }

        public static bool Login(string username, string password)
        {
            return SetPrincipal(User.ValidateLogin(username, password));
        }

        public static void LoadPrincipal(string username)
        {
            SetPrincipal(User.GetUser(username));
        }

        private static bool SetPrincipal(User identity)
        {
            if (!identity.IsAuthenticated) return identity.IsAuthenticated;
            var principal = new BdPrincipal(identity);
            ApplicationContext.User = principal;
            Thread.CurrentPrincipal = principal;
            return identity.IsAuthenticated;
        }

        public static void Logout()
        {
            ApplicationContext.User = new UnauthenticatedPrincipal();
        }
    }
}
