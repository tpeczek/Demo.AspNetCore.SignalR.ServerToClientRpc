using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using Demo.AspNetCore.SignalR.ServerToClientRpc.Contract;

namespace Demo.AspNetCore.SignalR.ServerToClientRpc.Server.Services
{
    internal class RpcCaller<THub> : IRpcCaller<THub>
        where THub : Hub<IRpcCalls>, IRpcResponseHandlers
    {
        private readonly IHubContext<THub, IRpcCalls> _hubContext;
        private readonly ConcurrentDictionary<Guid, TaskCompletionSource<MethodResponse>> _pendingMethodCalls = new ConcurrentDictionary<Guid, TaskCompletionSource<MethodResponse>>();

        public RpcCaller(IHubContext<THub, IRpcCalls> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task<MethodResponse> MethodCall(string userId, MethodParams methodParams)
        {
            methodParams.MethodCallId = Guid.NewGuid();

            TaskCompletionSource<MethodResponse> methodCallCompletionSource = new TaskCompletionSource<MethodResponse>();
            if (_pendingMethodCalls.TryAdd(methodParams.MethodCallId, methodCallCompletionSource))
            {
                await _hubContext.Clients.User(userId).MethodCall(methodParams);

                return await methodCallCompletionSource.Task;
            }

            throw new Exception("Couldn't call the method.");
        }

        public Task MethodResponseHandler(MethodResponse response)
        {
            if (_pendingMethodCalls.TryRemove(response.MethodCallId, out TaskCompletionSource<MethodResponse> methodCallCompletionSource))
            {
                methodCallCompletionSource.SetResult(response);
            }

            return Task.CompletedTask;
        }
    }
}
