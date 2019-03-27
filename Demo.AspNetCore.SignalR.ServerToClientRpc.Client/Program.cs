using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.AspNetCore.SignalR.ServerToClientRpc.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IHost host = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<RpcBackgroundService>();
                })
                .Build();

            await host.RunAsync();
        }
    }
}
