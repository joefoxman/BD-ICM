using Microsoft.VisualStudio.TestTools.UnitTesting;
using WHC.UnitTesting.MSTest;

namespace Bd.Icm.Tests
{
    public class AuthenticatedNoTransactionTest : TestBase
    {
        [TestInitialize]
        protected override void OnInitialize()
        {
            BdPrincipal.Login("testuser@test.com", "bd2016");
        }

        [TestCleanup]
        public override void OnCleanup()
        {
            BdPrincipal.Logout();
        }
    }
}
