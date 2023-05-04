using SuperSocket.SocketBase;
using SuperWebSocket;

namespace FarcanaWriterAPI.Services.WebSocketProcess
{
    public class WebSocketProcess
    {
        private WebSocketServer? Server;
        private WebSocketSession? SingleSession;
        private Thread SocketThread;

        public WebSocketProcess()
        {
            Server = new WebSocketServer();
            Server.Setup(8090);

            Server.NewSessionConnected += OnNewSessionConnected;
            Server.NewMessageReceived += OnNewMessageReceived;
            Server.SessionClosed += OnSessionClosed;

            Server.Start();

        }

        private void SocketThreadMain(object? o)
        {
            
        }

        public async Task<bool> IsReady()
        {
            int timeCount = 0;
            int timeout = 10;

            while(SingleSession == null && timeCount != timeout)
            {
                await Task.Delay(500);
                timeout++;
            }

            return SingleSession == null ? await Task.FromResult(false)
                : await Task.FromResult(true);
        }

        public void SendMessage(string message)
        {
            if(SingleSession != null && SingleSession.Connected)
            {
                SingleSession.Send(message);
            }
        }

        public void CloseConnection()
        {
            if (SingleSession != null && SingleSession.Connected)
            {
                SingleSession.Close();
                Server.Stop();
            }
        }

        private void OnNewSessionConnected(WebSocketSession session)
        {
            //if(SingleSession == null)
            //{
            //    SingleSession = session;
            //}
            //else
            //{
            //    ConsoleFormat.Fail("somebody try connect to establish session");
            //}
            ConsoleFormat.Info($"session {session.SessionID} connected");
        }

        private void OnNewMessageReceived(WebSocketSession session, string message)
        {
            Console.WriteLine($"Received message: {message}");
        }

        private void OnSessionClosed(WebSocketSession session, CloseReason reason)
        {
            switch (reason)
            {
                case CloseReason.ClientClosing: ConsoleFormat.Info($"session {session.SessionID} closed"); break;
                default: ConsoleFormat.Info($"session {session.SessionID} closed"); break;
            }
            
            session.Close();
        }
    }
}
