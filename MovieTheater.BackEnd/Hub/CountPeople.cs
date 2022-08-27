using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Hub
{
    public class CountAccessHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private const string Admin = "Admin";
        private static readonly HashSet<string> Connections = new HashSet<string>();

        public CountAccessHub():base()
        {
       
        }

        public override async Task OnConnectedAsync()
        {
            Connections.Add(Context.ConnectionId);
            UpdateAccess();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Connections.Remove(Context.ConnectionId);
            UpdateAccess();
        }

        public async Task AddToAdmin()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId,Admin);
            Connections.Remove(Context.ConnectionId);
            UpdateAccess();
        }

        private void UpdateAccess()
        {
            Clients.Group(Admin).SendAsync("updateAccess", Connections.Count);
        }
    }
}