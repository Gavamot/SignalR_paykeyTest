using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SignalRWebPack.Core.Rep;
#region snippet_HubsNamespace
using SignalRWebPack.Hubs;
#endregion

namespace SignalRWebPack
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddControllersAsServices();
            #region snippet_AddSignalR
            services.AddSignalR();
            #endregion
        }

        private void StartSwapToFile(FileMessageRep rep)
        {
            const int swapIntervalMs = 5000;
            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(swapIntervalMs);
                    rep.SaveChanges();
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var config = new Config();
            var fileMessageRep = new FileMessageRep(config.dbFileName);

            StartSwapToFile(fileMessageRep);

            DI.ConfigureContainer(fileMessageRep);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region snippet_UseStaticDefaultFiles
            app.UseDefaultFiles();
            app.UseStaticFiles();
            #endregion

            #region snippet_UseSignalR
            app.UseSignalR(options =>
            {
                options.MapHub<ChatHub>("/hub");
            });
            #endregion

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "default",
                template: "{controller}/{action}");
            });
        }
    }
}
