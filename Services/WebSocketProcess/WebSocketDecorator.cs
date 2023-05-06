using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace FarcanaWriterAPI.Services.WebSocketProcess
{

    public class WebSocketProcess : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = e.Data == "BALUS"
                      ? "Are you kidding?"
                      : "I'm not available now.";
            ConsoleFormat.Info(e.Data);
            Send(msg);
        }
    }
    public class WebSocketDecorator<T> where T : WebSocketBehavior, new()
    {
        private Thread WebSocketThread { get; set; }
        private WebSocketServer Server { get; set; }

        public void WebSocketThreadMain()
        {
            ConsoleFormat.Info("Создался WebSocket");
            Server = new WebSocketServer("ws://localhost:8030");
            Server.AddWebSocketService<T>("/process");
            Server.Start();
        }

        public async Task Stop()
        {
            if(Server == null)
                await Task.Delay(3000);

            if(Server != null)
            {
                Server.Stop();
                ConsoleFormat.Info("Сдох WebSocket");
            }
        }

        public void Send(string data)
        {
            #pragma warning disable CS0618
            if (Server != null && Server.IsListening)
            {
                Server.WebSocketServices.Broadcast(data);
            }
            #pragma warning restore CS0618
        }

        public void Start()
        {
            WebSocketThread = new Thread(WebSocketThreadMain);
            WebSocketThread.Start();
        }

    }
}
