using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Demo.AspNetCore.SignalR.ServerToClientRpc.Contract;

namespace Demo.AspNetCore.SignalR.ServerToClientRpc.Server.Services
{
    internal class RpcCallerBackgroundService : BackgroundService
    {
        private readonly IRpcCaller<RpcHub> _rpcCaller;

        public RpcCallerBackgroundService(IRpcCaller<RpcHub> rpcCaller)
        {
            _rpcCaller = rpcCaller;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);

                // This demo doesn't contain connected users management.
                // Please change null below to correct user identifier and make sure that user is connected before this line is called, otherwise it will hang.
                MethodResponse response = await _rpcCaller.MethodCall(null, new MethodParams
                {

                });
            }
        }
    }
}
