using System;

namespace Demo.AspNetCore.SignalR.ServerToClientRpc.Contract
{
    public class MethodParams
    {
        public Guid MethodCallId { get; set; }
    }
}
