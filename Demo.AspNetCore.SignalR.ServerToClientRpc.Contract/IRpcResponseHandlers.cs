using System.Threading.Tasks;

namespace Demo.AspNetCore.SignalR.ServerToClientRpc.Contract
{
    public interface IRpcResponseHandlers
    {
        Task MethodResponseHandler(MethodResponse response);
    }
}
