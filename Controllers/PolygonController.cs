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

        [HttpGet("GetArricalsTableData")]
        public async Task<IEnumerable<ArticalModel>> GetArricalsTableData()
        {
            var urls = await new PolygonWrapper().GetPolygonListURLs();
            return await new ArticalParser().GetArticalTableList(urls);
        }

        [HttpGet("GetArricals")]
        public async Task<IEnumerable<ArticalModel>> GetArricals()
        {
            var urls = await new PolygonWrapper().GetPolygonListURLs();
            return await new ArticalParser().GetArticalList(urls);
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
