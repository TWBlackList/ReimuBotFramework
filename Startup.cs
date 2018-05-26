using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ReimuBotFramework.ReimuBase;

namespace ReimuBotFramework
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            BotFramework botFramework = new BotFramework();
            app.Run(async context =>
            {
                context.Response.ContentType = "text/plain";
                string filePath = context.Request.Path;
                if (filePath.Length >= 16)
                {
                    string jsonString = new StreamReader(context.Request.Body).ReadToEnd();
                    string path = filePath.Substring(0, 16).ToLower();
                    if (path == "/teleapireceiver")
                    {
                        botFramework.NewRequest(jsonString);
                    }
                    else if (path == "/tw")
                    {
                        botFramework.NewRequest(jsonString);
                    }
                    else if (path == "/cn")
                    {
                        botFramework.NewRequest(jsonString);
                    }
                    else if (path == "/jorden")
                    {
                        botFramework.NewRequest(jsonString);
                    }
                    else if (path == "/dev")
                    {
                        botFramework.NewRequest(jsonString);
                    }
                    else if (path == "/pluginsreceiver")
                    {
                        // TODO: 加入插件的 WebHook 支持
                    }
                }

                await context.Response.WriteAsync("a");
            });
        }
    }
}