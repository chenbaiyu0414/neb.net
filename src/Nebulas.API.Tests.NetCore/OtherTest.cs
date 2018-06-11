using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nebulas.API.Tests.NetCore
{
    [TestClass]
    public class OtherTest
    {
        [TestMethod]
        public void TestAccount()
        {
            var account = new NebAccount("ac3773e06ae74c0fa566b0e421d4e391333f31aef90b383f0c0e83e4873609d6");

            Assert.AreEqual("ac3773e06ae74c0fa566b0e421d4e391333f31aef90b383f0c0e83e4873609d6", account.GetPrivateKeyString(), true);

            Assert.AreEqual(
                "20ba34f53b39cb6e43b68a46f1fc2cf987d8fb362eba158c6d4685de7e6e9daca5d6dd1326f5e902562e2661785b6b94c1566b75b7383f3662e2a2399d2c8d09",
                account.GetPublicKeyString(), true);

            Assert.AreEqual("19574384dc49e3e3b360d5abda27fde4c012aed123d14d279a06",
                BitConverter.ToString(account.GetAddress()).Replace("-", ""), true);

            Assert.AreEqual("n1LfrjZzXDCcHhNV2r6F6dUS5Zxi7P8xC45", account.GetAddressString(), true);
        }

        [TestMethod]
        public void TestContractDeployHash()
        {
            var acc = NebAccount.FromAddress("n1LfrjZzXDCcHhNV2r6F6dUS5Zxi7P8xC45");

            var contract = new NebContract
            {
                Content = new NebContract.DeployContent
                {
                    SourceType = NebContract.DeployContent.ContentSourceType.Js,
                    Source = "naosgnoa"
                }
            };

            var transaction = new NebTransaction(1, acc, acc, "1", 1, "1000", "1000", contract);

            Assert.AreEqual("b799dd06c824b30331a7ef708b07194ec4febb79c3433832d7629a1418ae7a1a",
                ToHexString(transaction.GetTransactionHash()));
        }

        [TestMethod]
        public void TestContractCallHash()
        {
            var acc = NebAccount.FromAddress("n1LfrjZzXDCcHhNV2r6F6dUS5Zxi7P8xC45");

            var contract = new NebContract
            {
                Content = new NebContract.CallContent
                {
                    Function = "js",
                    Args = new object[] { "naosgnoa" }
                }
            };

            var transaction = new NebTransaction(1, acc, acc, "1", 1, "1000", "1000", contract);

            Assert.AreEqual("41926966d7b4c2a1fb27669346e5f73a0b78304237d8b92bd63d13882b9b4138",
                ToHexString(transaction.GetTransactionHash()));
        }

        [TestMethod]
        public void TestSignTransaction()
        {
            var acc = new NebAccount("ac3773e06ae74c0fa566b0e421d4e391333f31aef90b383f0c0e83e4873609d6");

            var contract = new NebContract
            {
                Content = new NebContract.CallContent
                {
                    Function = "js",
                    Args = new object[] { "naosgnoa" }
                }
            };

            var transaction = new NebTransaction(1, acc, acc, "1", 1, "1000", "1000", contract);

            Assert.AreEqual(
                "be91c0071533cf8ec51cc534ffc425d0a498306bc1b03e2b54bcf25ce33af6b6035cc3bb727f1d563e6800fdfe582f7e7895638e6b22b315d2e5ffe0af1c5c5100",
                ToHexString(transaction.SignTransaction()));
        }

        private string ToHexString(byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", "").ToLower();
        }
    }
}
