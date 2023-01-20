using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Robot.Abstractions.Services;
using System.Text;

namespace Robot.CDT.Services;

internal class CDTHTMLParseService : IHTMLParseService
{
    private readonly ILogger<CDTHTMLParseService> logger;

    public CDTHTMLParseService(ILogger<CDTHTMLParseService> logger)
    {
        this.logger = logger;
    }

    public HtmlDocument Parse(string url)
    {
        // From Web
        var web = new HtmlWeb();
        return web.Load(url);
    }
}
