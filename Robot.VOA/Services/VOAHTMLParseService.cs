using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Robot.Abstractions.Services;
using System.Text;

namespace Robot.VOA.Services;

internal class VOAHTMLParseService : IHTMLParseService
{
    private readonly ILogger<VOAHTMLParseService> logger;

    public VOAHTMLParseService(ILogger<VOAHTMLParseService> logger)
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
