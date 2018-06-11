using Newtonsoft.Json;

namespace Nebulas.API.Responses.Admin
{
    public class ConfigResponse
    {
        public class InternalConfig
        {
            public class NetworkConfig
            {
                [JsonProperty("seed")]
                public string[] Seed { get; set; }

                [JsonProperty("listen")]
                public string[] Listen { get; set; }

                [JsonProperty("private_key")]
                public string PrivateKey { get; set; }

                [JsonProperty("network_id")]
                public uint NetworkId { get; set; }
            }

            public class ChainConfig
            {
                [JsonProperty("chain_id")]
                public uint ChainId { get; set; }

                [JsonProperty("genesis")]
                public string Genesis { get; set; }

                [JsonProperty("datadir")]
                public string DataDir { get; set; }

                [JsonProperty("keydir")]
                public string KeyDir { get; set; }

                [JsonProperty("start_mine")]
                public bool IsStartMine { get; set; }

                [JsonProperty("coinbase")]
                public string Coinbase { get; set; }

                [JsonProperty("miner")]
                public string Miner { get; set; }

                [JsonProperty("passphrase")]
                public string Passphrase { get; set; }

                [JsonProperty("enable_remote_sign_server")]
                public bool IsEnableRemoteSignServer { get; set; }

                [JsonProperty("remote_sign_server")]
                public string RemoteSignServer { get; set; }

                [JsonProperty("gas_price")]
                public string GasPrice { get; set; }

                [JsonProperty("gas_limit")]
                public string GasLimit { get; set; }

                [JsonProperty("signature_ciphers")]
                public string[] SignatureCiphers { get; set; }
            }

            public class RpcConfig
            {
                [JsonProperty("rpc_listen")]
                public string[] RpcListen { get; set; }

                [JsonProperty("http_listen")]
                public string[] HttpListen { get; set; }

                [JsonProperty("http_module")]
                public string[] HttpModule { get; set; }

                [JsonProperty("connection_limits")]
                public uint ConnectionLimits { get; set; }

                [JsonProperty("http_limits")]
                public uint HttpLimits { get; set;}

                [JsonProperty("http_cors")]
                public string[] HttpCors { get; set; }
            }

            public class StatsConfig
            {
                public class InfluxDbConfig
                {
                    [JsonProperty("host")]
                    public string Host { get; set; }

                    [JsonProperty("port")]
                    public string Port { get; set; }

                    [JsonProperty("db")]
                    public string Db { get; set; }

                    [JsonProperty("user")]
                    public string User { get; set; }

                    [JsonProperty("password")]
                    public string Password { get; set; }
                }

                [JsonProperty("enable_metrics")]
                public bool IsEnableMetrics { get; set; }

                [JsonProperty("reporting_module")]
                public string[] ReportingModule { get; set; }

                [JsonProperty("influxdb")]
                public InfluxDbConfig InfluxDb { get; set; }

                [JsonProperty("metrics_tags")]
                public string[] MetricsTags { get; set; }
            }

            public class AppConfig
            {
                public class PprofConfig
                {
                    [JsonProperty("http_listen")]
                    public string HttpListen { get; set; }

                    [JsonProperty("cpuprofile")]
                    public string CpuProfile { get; set; }

                    [JsonProperty("memprofile")]
                    public string MemProfile { get; set; }
                }

                [JsonProperty("log_level")]
                public string LogLevel { get; set; }

                [JsonProperty("log_file")]
                public string LogFile { get; set; }

                [JsonProperty("log_age")]
                public uint LogAge { get; set; }

                [JsonProperty("enable_crash_report")]
                public bool IsEnableCrashReport { get; set; }

                [JsonProperty("crash_report_url")]
                public string CrashReportUrl { get; set; }

                [JsonProperty("pprof")]
                public PprofConfig Pprof { get; set; }

                [JsonProperty("Version")]
                public string Version { get; set; }
            }

            [JsonProperty("network")]
            public NetworkConfig Network { get; set; }

            [JsonProperty("chain")]
            public ChainConfig Chain { get; set; }

            [JsonProperty("rpc")]
            public RpcConfig Rpc { get; set; }

            [JsonProperty("stats")]
            public StatsConfig Stats { get; set; }

            [JsonProperty("misc")]
            public string Misc { get; set; }

            [JsonProperty("app")]
            public AppConfig App { get; set; }
        }

        [JsonProperty("config")]
        public InternalConfig Config { get; set; }
    }
}
