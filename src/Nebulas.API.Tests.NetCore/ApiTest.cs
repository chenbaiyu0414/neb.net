using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nebulas.API.Tests.NetCore
{
    [TestClass]
    public class ApiTest
    {
        private readonly Neb m_neb = new Neb();

        [TestMethod]
        public void TestGetNebState()
        {
            var nebState = m_neb.Api.GetNebState();
            Assert.AreEqual<uint>(1, nebState.ChainId);
        }

        [TestMethod]
        public void TestGetAccountState()
        {
            var accountState = m_neb.Api.GetAccountState("n1VVnuQSKbiCk9dieuAqGRsCxYdMZEA8yG4");
            Assert.AreEqual(NebAccount.AddressType.Normal, accountState.Type);
        }
    }
}
