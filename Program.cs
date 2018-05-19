using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using ReimuAPI.ReimuBase;

namespace ReimuBotFramework
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls(new ConfigManager().getConfig().bind)
                .Build();
        }
    }
}