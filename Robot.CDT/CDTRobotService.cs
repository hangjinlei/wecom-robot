using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Robot.Abstractions;
using Robot.Abstractions.Factories;
using Robot.Abstractions.Services;
using Robot.CDT.Factories;
using Robot.CDT.Models;
using Robot.CDT.Services;
using System.Net.Http.Json;

namespace Robot.CDT;

internal class CDTRobotService : IRobotService
{
    private readonly IEnumerable<ICountdownService> countdownService;
    private readonly IEnumerable<ITimerFactory> timerFactory;
    private readonly IEnumerable<IHTMLParseService> htmlParseService;
    private readonly IOptions<List<New>> options;
    private readonly IConfiguration configuration;

    public CDTRobotService(IEnumerable<ICountdownService> countdownService, IEnumerable<ITimerFactory> timerFactory, IEnumerable<IHTMLParseService> htmlParseService, IOptions<List<New>> options, IConfiguration configuration)
    {
        this.countdownService = countdownService;
        this.timerFactory = timerFactory;
        this.htmlParseService = htmlParseService;
        this.options = options;
        this.configuration = configuration;
    }

    public void Run()
    {
        var voaCountdownService = countdownService.FirstOrDefault(x => x is CDTCountdownService);
        var voaTimerFactory = timerFactory.FirstOrDefault(x => x is CDTTimerFactory);
        var voaHTMLParseService = htmlParseService.FirstOrDefault(x => x is CDTHTMLParseService);

        if (voaCountdownService is { } && voaTimerFactory is { })
        {
            var timer = voaTimerFactory.CreateTimer(async (state) =>
            {
                var content = voaHTMLParseService?.Parse("https://www.voachinese.com/");

                var webhook = configuration.GetValue<string>("Webhook");
                var client = new HttpClient();
                var response = await client.PostAsJsonAsync(webhook,
                    new
                    {
                        msgtype = "text",
                        text = new
                        {
                            content
                        }
                    });
            });
            voaCountdownService.Countdown(timer);
        }
    }
}
