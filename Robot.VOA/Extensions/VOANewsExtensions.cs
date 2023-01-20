using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Robot.Abstractions;
using Robot.Abstractions.Factories;
using Robot.Abstractions.Services;
using Robot.VOA.Factories;
using Robot.VOA.Services;

namespace Robot.VOA.Extensions;

public static class VOANewsExtensions
{
    public static IServiceCollection AddVOANews(this IServiceCollection services, HostBuilderContext context)
    {
        services.AddOptions().Configure<Models.VOA>(m => context.Configuration.GetSection("voa").Bind(m));

        services.AddSingleton<ICountdownService, VOACountdownService>();
        services.AddSingleton<ITimerFactory, VOATimerFactory>();
        services.AddSingleton<IRobotService, VOARobotService>();
        services.AddSingleton<IHTMLParseService, VOAHTMLParseService>();

        return services;
    }
}
