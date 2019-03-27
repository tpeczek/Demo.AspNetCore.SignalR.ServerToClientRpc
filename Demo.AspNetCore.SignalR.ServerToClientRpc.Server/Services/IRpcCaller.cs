using Microsoft.AspNetCore.SignalR;
using Demo.AspNetCore.SignalR.ServerToClientRpc.Contract;

namespace Demo.AspNetCore.SignalR.ServerToClientRpc.Server.Services
{
    public interface IRpcCaller<THub> : IRpc, IRpcResponseHandlers
        where THub : Hub<IRpcCalls>, IRpcResponseHandlers
    { }
}
