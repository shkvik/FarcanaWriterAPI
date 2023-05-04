using AngleSharp;
using AngleSharp.Html.Dom;
using Colorify;
using FarcanaWriterAPI.Services.WebSocketProcess;
using Microsoft.Extensions.Configuration;
using IConfiguration = AngleSharp.IConfiguration;

namespace FarcanaMarketUtility.Polygon
{
    class PolygonWrapper
    {

        private IConfiguration _Configuration => Configuration.Default.WithDefaultLoader();
        private IBrowsingContext _BrowsingContext => BrowsingContext.New(_Configuration);

        private string PolygonURL = "https://www.polygon.com/";
        private string PolygonArchiveURL = "https://www.polygon.com/archives/";

        public async Task<List<string>> GetPolygonListURLs()
        {
            var polygons = new List<string>();


            polygons.AddRange(await GetLinksFromMainPage());
            polygons.AddRange(await GetLinksFromArchivePages());

            foreach (var (polygon, index) in polygons.Select((value, i) => (value, i)))
            {
                ConsoleFormat.Info($"parsed [{index}] {polygon}");
            }

            return polygons;
        }

        public async Task<IEnumerable<string>?> GetLinksFromMainPage()
        {
            try
            {
                var document = await _BrowsingContext.OpenAsync(PolygonURL);
                var cellSelector = "a.c-entry-box--compact__image-wrapper";
                var cells = document.QuerySelectorAll(cellSelector);
                return cells.Select(m => ((IHtmlAnchorElement)m).Href);
            }
            catch (Exception ex)
            {
                ConsoleFormat.Fail("url parsing exception :" + ex.Message);
            }
            return null;
        }


        public async Task<IEnumerable<string>?> GetLinksFromArchivePages()
        {
            var countDebug = 16;

            var archives = new List<string>();
            //var wsProcess = new WebSocketProcess();



            using (var progress = new ProgressBar())
            {
                for (int i = 1; i < countDebug; i++)
                {
                    try
                    {
                        var progressCount = (double)i / countDebug;

                        progress.Report(progressCount);
                        var document = await _BrowsingContext.OpenAsync(String.Format(PolygonArchiveURL + $"{i}"));
                        var cellSelector = "a.c-entry-box--compact__image-wrapper";
                        var cells = document.QuerySelectorAll(cellSelector);
                        archives.AddRange(cells.Select(m => ((IHtmlAnchorElement)m).Href));

                        //wsProcess.SendMessage(Convert.ToString(progressCount));
                        
                    }
                    catch (Exception ex)
                    {
                        ConsoleFormat.Fail("url parsing exception :" + ex.Message);
                        continue;
                    }
                }
            }
            //wsProcess.CloseConnection();
            return archives;
        }
    }
}
