using FarcanaMarketUtility.Polygon;
using FarcanaWriterAPI.Services.OpenAI;
using FarcanaWriterAPI.Services.WebSocketProcess;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FarcanaWriterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolygonController : ControllerBase
    {

        [HttpGet("GetPolygonUrls")]
        public async Task<IEnumerable<string>> GetPolygonUrls()
        {
            var webSocket = new WebSocketDecorator<WebSocketProcess>();
            return await new PolygonWrapper().GetPolygonListURLs(webSocket.Send);
        }

        [HttpGet("UpdateArticalsData")]
        public async Task<bool> UpdateArticalsData()
        {
            try
            {
                var webSocket = new WebSocketDecorator<WebSocketProcess>();
                webSocket.Start();
                var urls = await new PolygonWrapper().GetPolygonListURLs(webSocket.Send);
                var articals = await new ArticalParser().GetArticalList(urls, webSocket.Send);

                Storage.Update(articals);
                await webSocket.Stop();

                return true;
            }
            catch (Exception ex)
            {
                ConsoleFormat.Fail(ex.Message);
                return false;
            }
        }


        [HttpGet("GetArticalsTableData")]
        public IEnumerable<ArticalModel> GetArticalsTableData()
        {
            return Storage.GetAll();
        }

        [HttpGet("GetArticals")]
        public IEnumerable<ArticalModel> GetArricals()
        {
            return Storage.GetAll();
        }

        [HttpGet("GetArtical/{id}")]
        public ArticalModel? GetArtical(int id)
        {
            return Storage.GetItemById(id);
        }

        [HttpGet("testing")]
        public async Task<string> Testing()
        {
            //var test = new RewriterAdapter();
            //await test.Test();
            var test = new WebSocketDecorator<WebSocketProcess>();
            test.Start();
            for(int i = 0;i< 5; i++)
            {
                await Task.Delay(1000);
                test.Send("message");
            }
           
            await test.Stop();
            return "value";
        }
    }
}
