using System;

namespace Bd.Icm.Web
{
    public class WebApiAuthorizationException : Exception
    {
        public WebApiAuthorizationException(string message) : base(message)
        {
        }
    }
}
