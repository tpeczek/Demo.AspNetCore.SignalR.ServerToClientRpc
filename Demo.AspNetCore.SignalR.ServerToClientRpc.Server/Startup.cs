using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Demo.AspNetCore.SignalR.ServerToClientRpc.Server.Services;

namespace Demo.AspNetCore.SignalR.ServerToClientRpc.Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IRpcCaller<>), typeof(RpcCaller<>));

            services.AddSignalR();

            services.AddHostedService<RpcCallerBackgroundService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSignalR(routes =>
            {
                routes.MapHub<RpcHub>("/rpc");
            });
        }
    }
}
