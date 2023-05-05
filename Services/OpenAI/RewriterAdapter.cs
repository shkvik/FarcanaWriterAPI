using OpenAI_API;

namespace FarcanaWriterAPI.Services.OpenAI
{
    public class RewriterAdapter
    {
        private OpenAIAPI API { get; set; }
        public RewriterAdapter()
        {
            API = new OpenAIAPI("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");
        }

        public async Task Test()
        {
            var chat = API.Chat.CreateConversation();
            chat.AppendUserInput("How to make a hamburger?");

            await foreach (var res in chat.StreamResponseEnumerableFromChatbotAsync())
            {
                Console.Write(res);
            }
        }
    }
}
