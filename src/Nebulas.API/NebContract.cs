using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nebulas.API
{
    public class NebContract
    {
        public interface INebContractContent
        {

        }

        public class DeployContent : INebContractContent
        {
            public enum ContentSourceType
            {
                Js,
                Ts
            }

            [JsonConverter(typeof(StringEnumConverter),true)]
            public ContentSourceType SourceType { get; set; }
            
            public string Source { get; set; }

            public object[] Args { get; set; }
        }

        public class CallContent : INebContractContent
        {
            public string Function { get; set; }

            public object[] Args { get; set; }
        }

        public class BinaryContent : INebContractContent
        {
            public byte[] Data { get; set; }
        }

        public static NebContract DefaultContract { get; } = new NebContract()
        {
            Content = new BinaryContent
            {
                Data = new byte[0]
            }
        };

        public INebContractContent Content { get; set; }
    }
}
