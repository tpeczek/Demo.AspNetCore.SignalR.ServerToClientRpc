using System.Threading.Tasks;

namespace Demo.AspNetCore.SignalR.ServerToClientRpc.Contract
{
    public interface IRpc
    {
        Task<MethodResponse> MethodCall(string userId, MethodParams methodParams);
    }
}
