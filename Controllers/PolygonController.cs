using FarcanaMarketUtility.Polygon;
using FarcanaWriterAPI.Services.OpenAI;
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
            return await new PolygonWrapper().GetPolygonListURLs();
        }

        [HttpGet("UpdateArticalsData")]
        public async Task<bool> UpdateArticalsData()
        {
            try
            {
                var urls = await new PolygonWrapper().GetPolygonListURLs();
                var articals = await new ArticalParser().GetArticalList(urls);

                Storage.Update(articals);
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
            var test = new RewriterAdapter();
            await test.Test();

            return "value";
        }
    }
}
