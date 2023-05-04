using FarcanaMarketUtility.Polygon;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FarcanaWriterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolygonController : ControllerBase
    {
        // GET: api/<PolygonController>
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
        public async Task<IEnumerable<ArticalModel>> GetArticalsTableData()
        {
            var urls = await new PolygonWrapper().GetPolygonListURLs();
            return await new ArticalParser().GetArticalTableList(urls);
        }

        [HttpGet("GetArricals")]
        public IEnumerable<ArticalModel> GetArricals()
        {
            return Storage.Get();
        }

        // GET api/<PolygonController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("testing")]
        public string Testing()
        {
            return "value";
        }
    }
}
