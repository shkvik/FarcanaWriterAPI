using Newtonsoft.Json;

public class JsonRpcRequest<T>
{
    [JsonProperty("jsonrpc")]
    public string Jsonrpc { get => "2.0"; }

    [JsonProperty("method")]
    public string Method { get; set; }

    [JsonProperty("params")]
    public List<T> Params { get; set; }

    [JsonProperty("id")]
    public long Id { get => DateTime.Now.Ticks; }
}

public class JsonRpcResponse<T>
{
    [JsonProperty("jsonrpc")]
    public string Jsonrpc { get => "2.0"; }

    [JsonProperty("result")]
    public T Result { get; set; }

    [JsonProperty("id")]
    public long Id
    {
        get
        {
            // Вычисляем разницу между текущей датой и временем и 1 января 1970 года
            TimeSpan timeSpan = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            // Получаем количество секунд, прошедших с 1 января 1970 года
            return (long)timeSpan.TotalSeconds;
        }

    }
}