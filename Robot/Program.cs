using Robot;
using Robot.CDT.Extensions;
using Robot.VOA.Extensions;

var builder = Host.CreateDefaultBuilder(args);

IHost host = builder.ConfigureServices((context, services) =>
    {
        services.AddVOANews(context); // Add VOANews
        //services.AddCDTNews(context); // Add CDTNews

        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
