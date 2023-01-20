using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Robot.Abstractions;
using Robot.Abstractions.Factories;
using Robot.Abstractions.Services;
using Robot.VOA.Factories;
using Robot.VOA.Services;
using System.Net.Http.Json;
using System.Text;

namespace Robot.VOA;

internal class VOARobotService : IRobotService
{
    private readonly IEnumerable<ICountdownService> countdownService;
    private readonly IEnumerable<ITimerFactory> timerFactory;
    private readonly IEnumerable<IHTMLParseService> htmlParseService;
    private readonly IOptions<Models.VOA> options;
    private readonly IConfiguration configuration;

    public VOARobotService(IEnumerable<ICountdownService> countdownService, IEnumerable<ITimerFactory> timerFactory, IEnumerable<IHTMLParseService> htmlParseService, IOptions<Models.VOA> options, IConfiguration configuration)
    {
        this.countdownService = countdownService;
        this.timerFactory = timerFactory;
        this.htmlParseService = htmlParseService;
        this.options = options;
        this.configuration = configuration;
    }

    public void Run()
    {
        var voaCountdownService = countdownService.FirstOrDefault(x => x is VOACountdownService);
        var voaTimerFactory = timerFactory.FirstOrDefault(x => x is VOATimerFactory);
        var voaHTMLParseService = htmlParseService.FirstOrDefault(x => x is VOAHTMLParseService);

        if (voaCountdownService is { } && voaTimerFactory is { })
        {
            var timer = voaTimerFactory.CreateTimer(async (state) =>
            {
                var doc = voaHTMLParseService?.Parse(url: options.Value.url);

                var sb = new StringBuilder();

                sb.AppendLine($"# 数据来源：{options.Value.title}\n");

                foreach (var content in options.Value.content)
                {
                    sb.AppendLine($"## {content.category}");
                    var index = 1;
                    foreach (var newsNode in doc.DocumentNode.SelectNodes(content.html.newsPath))
                    {
                        string? title = null;
                        string? src = null;

                        if (!string.IsNullOrWhiteSpace(content.html.titleAttribute))
                        {
                            title = newsNode.Attributes[content.html.titleAttribute].Value;
                        }
                        else
                        {
                            title = newsNode.ChildNodes["h4"].Attributes[content.html.title.relativeAttribute].Value;//.SelectNodes(content.html.title.relativeXPath)[0].Attributes[content.html.title.relativeAttribute].Value;
                        }

                        src = newsNode.Attributes[content.html.srcAttribute].Value;

                        var news = $"{index++}. [{title.Trim().Replace("\n", "")}]({options.Value.url.TrimEnd('/')}{src.Trim()})";

                        sb.AppendLine(news);
                    }
                    sb.AppendLine();
                }

                var messageContent = sb.ToString();

                var webhook = configuration.GetValue<string>("Webhook");
                var client = new HttpClient();
                var response = await client.PostAsJsonAsync(webhook,
                    new
                    {
                        msgtype = "text",
                        text = new
                        {
                            content = messageContent
                        }
                    });
            });
            voaCountdownService.Countdown(timer, period: 1000 * 60 * 60 * 24);
        }
    }
}
