using WHC.UnitTesting.MSTest;

namespace Bd.Icm.Tests
{
    public class AuthenticatedTest : TransactionalTest
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();
            BdPrincipal.Login("testuser@test.com", "1");
        }

        public override void OnCleanup()
        {
            BdPrincipal.Logout();
            base.OnCleanup();
        }
    }
}
