using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Colorify;
using FarcanaWriterAPI.Services.WebSocketProcess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IConfiguration = AngleSharp.IConfiguration;


class Info
{
    [JsonProperty("current")]
    public int Current { get; set; }

    [JsonProperty("total")]
    public int Total { get; set; }
}

class ArticalParser
{
    private List<ArticalModel>? ArticalModels { set; get; }
    private IConfiguration _Configuration => Configuration.Default.WithDefaultLoader();
    private IBrowsingContext _BrowsingContext => BrowsingContext.New(_Configuration);

    public async Task<List<ArticalModel>> GetArticalList(List<string> urls, Action<string> webSocketSend)
    {
        var articalList = new List<ArticalModel>();

        ConsoleFormat._colorify.DivisionLine('-', Colors.bgDefault);
        ConsoleFormat.Info($"start fill articals model");

        //foreach (var (url, index) in urls.Select((value, i) => (value, i)))
        var dbgCount = 10;

        for (int index = 0; index < dbgCount; index++)
        {

            try
            {
                var document = await _BrowsingContext.OpenAsync(urls[index]);

                var articalModel = new ArticalModel
                {
                    Id          = index,
                    Title       = document.QuerySelector("h1.c-page-title").Text(),
                    Description = document.QuerySelector("p.c-entry-summary.p-dek").Text(),
                    Author      = document.QuerySelector("span.c-byline__author-name").Text(),
                    Date        = document.QuerySelector("time.c-byline__item").GetAttribute("datetime"),
                    Article     = document.QuerySelector("div.c-entry-content ").Text()
                };

                articalList.Add(articalModel);

                ConsoleFormat.Info($"[{index}] success loaded [{articalModel.Title} | {articalModel.Date}]");

                var processMsg = new ProcessMessage()
                {
                    Step = Step.ContentParsing,
                    Count = (float)index / dbgCount
                };

                webSocketSend(JsonConvert.SerializeObject(processMsg));

            }
            catch (Exception ex)
            {
                ConsoleFormat.Fail($"[{index}] content error parsing: " + $"({ex.Message})");
                continue;
            }
        }
        ConsoleFormat.Info($"amount of success articals [{articalList.Count}]");
        return articalList;
    }

    public async Task<List<ArticalModel>> GetArticalTableList(List<string> urls)
    {
        var articalList = new List<ArticalModel>();

        ConsoleFormat._colorify.DivisionLine('-', Colors.bgDefault);
        ConsoleFormat.Info($"start fill articals model");

        //foreach (var (url, index) in urls.Select((value, i) => (value, i)))
        for (int index = 0; index < 10; index++)
        {

            try
            {
                var document = await _BrowsingContext.OpenAsync(urls[index]);
                var articalModel = new ArticalModel
                {
                    Id          = index,
                    Title       = document.QuerySelector("h1.c-page-title").Text(),
                    Description = document.QuerySelector("p.c-entry-summary.p-dek").Text(),
                    Author      = document.QuerySelector("span.c-byline__author-name").Text(),
                    Date        = document.QuerySelector("time.c-byline__item").GetAttribute("datetime"),
                };
                articalList.Add(articalModel);
                ConsoleFormat.Info($"[{index}] success loaded [{articalModel.Title} | {articalModel.Date}]");
            }
            catch (Exception ex)
            {
                ConsoleFormat.Fail($"[{index}] content error parsing: " + $"({ex.Message})");
                continue;
            }
        }
        ConsoleFormat.Info($"amount of success articals [{articalList.Count}]");
        return articalList;
    }
}

