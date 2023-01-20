using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Robot.Abstractions;
using Robot.Abstractions.Factories;
using Robot.Abstractions.Services;
using Robot.CDT.Factories;
using Robot.CDT.Models;
using Robot.CDT.Services;

namespace Robot.CDT.Extensions;

public static class CDTNewsExtensions
{
    public static IServiceCollection AddCDTNews(this IServiceCollection services, HostBuilderContext context)
    {
        services.AddOptions().Configure<List<New>>(context.Configuration.GetSection("News"));

        services.AddSingleton<ICountdownService, CDTCountdownService>();
        services.AddSingleton<ITimerFactory, CDTTimerFactory>();
        services.AddSingleton<IRobotService, CDTRobotService>();
        services.AddSingleton<IHTMLParseService, CDTHTMLParseService>();

        return services;
    }
}
