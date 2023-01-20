using System.Net.Http.Json;


Console.WriteLine($"[Starting timer...] - {DateTime.Now}");

StartNewTimer().Start();

Console.ReadKey();

System.Timers.Timer StartNewTimer()
{
    var interval = Random.Shared.Next(1000 * 60 * 5, 1000 * 60 * 15);
    var timer = new System.Timers.Timer(interval: interval);
    var nextTime = DateTime.Now.AddMilliseconds(interval);
    Console.WriteLine($"[Next Time] - {nextTime}");

    timer.Elapsed += async (sender, e) =>
    {
        var downTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 45, 00);
        var currentTime = DateTime.Now;
        var remainTime = downTime - currentTime;

        Console.WriteLine($"[Timer elapsed] - {currentTime}");

        var remainMinutes = Convert.ToInt32(remainTime.TotalMinutes);

        var client = new HttpClient();
        var webhook = "";
        var response = await client.PostAsJsonAsync("https://qyapi.weixin.qq.com/cgi-bin/webhook/send?key=934011f8-05bc-4a00-9d1d-1ee9ddad2bd6",
            new
            {
                msgtype = "text",
                text = new
                {
                    content = remainMinutes
                }
            });

        StartNewTimer().Start();
        timer.Stop();
        timer.Dispose();
    };

    return timer;
}