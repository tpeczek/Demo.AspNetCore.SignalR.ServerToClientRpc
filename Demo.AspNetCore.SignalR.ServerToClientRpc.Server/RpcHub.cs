using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Demo.AspNetCore.SignalR.ServerToClientRpc.Contract;
using Demo.AspNetCore.SignalR.ServerToClientRpc.Server.Services;

namespace Demo.AspNetCore.SignalR.ServerToClientRpc.Server
{
    public class RpcHub : Hub<IRpcCalls>, IRpcResponseHandlers
    {
        private readonly IRpcCaller<RpcHub> _rpcCaller;

        public RpcHub(IRpcCaller<RpcHub> rpcCaller)
        {
            _rpcCaller = rpcCaller;
        }

        public Task MethodResponseHandler(MethodResponse response)
        {
            return _rpcCaller.MethodResponseHandler(response);
        }
    }
}
