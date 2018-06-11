using Nebulas.API.Providers.Http;

namespace Nebulas.API
{
    public sealed class Neb
    {
        public const string MAIN_NET = "https://mainnet.nebulas.io";
        public const string TEST_NET = "https://testnet.nebulas.io";

        public NebApi Api { get; }
        public NebAdmin Admin { get; }

        public Neb(string host = MAIN_NET, string apiVersion = "v1")
        {
            Api = new NebApi(new NebHttpProvider(host, apiVersion, "user"));
            Admin = new NebAdmin(new NebHttpProvider(host, apiVersion, "admin"));
        }
    }
}
