using Newtonsoft.Json;

namespace FarcanaWriterAPI.Services.WebSocketProcess
{
    public enum Step 
    {
        LinkParsing,
        ContentParsing,
    }

    public class ProcessMessage
    {
        [JsonProperty("step")]
        public Step Step { get; set; }

        [JsonProperty("count")]
        public float Count { get; set; }

    }
}
