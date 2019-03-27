using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Demo.AspNetCore.SignalR.ServerToClientRpc.Contract;

namespace Demo.AspNetCore.SignalR.ServerToClientRpc.Client
{
    internal class RpcBackgroundService : BackgroundService
    {
        private readonly HubConnection _rpcResponseHubConnection;

        public RpcBackgroundService()
        {
            _rpcResponseHubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/rpc")
                .Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IDisposable _methodCallHandler = _rpcResponseHubConnection.On<MethodParams>(nameof(IRpcCalls.MethodCall), methodParams =>
            {
                Task.Delay(1000).Wait();

                _rpcResponseHubConnection.InvokeAsync(nameof(IRpcResponseHandlers.MethodResponseHandler), new MethodResponse
                {
                    MethodCallId = methodParams.MethodCallId
                });
            });

            await _rpcResponseHubConnection.StartAsync();

            await WaitForCancellationAsync(stoppingToken);

            _methodCallHandler.Dispose();

            await _rpcResponseHubConnection.DisposeAsync();
        }

        private async Task WaitForCancellationAsync(CancellationToken cancellationToken)
        {
            TaskCompletionSource<bool> cancelationTaskCompletionSource = new TaskCompletionSource<bool>();
            cancellationToken.Register(taskCompletionSource => ((TaskCompletionSource<bool>)taskCompletionSource).SetResult(true), cancelationTaskCompletionSource);

            try
            {
                await (cancellationToken.IsCancellationRequested ? Task.CompletedTask : cancelationTaskCompletionSource.Task);
            }
            catch (OperationCanceledException)
            { }
        }
    }
}
