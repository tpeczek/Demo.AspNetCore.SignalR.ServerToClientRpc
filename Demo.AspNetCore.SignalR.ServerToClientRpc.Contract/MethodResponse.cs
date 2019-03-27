using System;

namespace Demo.AspNetCore.SignalR.ServerToClientRpc.Contract
{
    public class MethodResponse
    {
        public Guid MethodCallId { get; set; }
    }
}
