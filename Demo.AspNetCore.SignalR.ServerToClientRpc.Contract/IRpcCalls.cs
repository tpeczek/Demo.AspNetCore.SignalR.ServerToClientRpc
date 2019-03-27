using System.Threading.Tasks;

namespace Demo.AspNetCore.SignalR.ServerToClientRpc.Contract
{
    public interface IRpcCalls
    {
        Task MethodCall(MethodParams methodParams);
    }
}
