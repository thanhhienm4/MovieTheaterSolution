using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MovieTheater.BackEnd.Hub
{
    public class CountAccessHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private const string Admin = "Admin";
        private static readonly HashSet<string> _connections = new HashSet<string>();

        public CountAccessHub():base()
        {
       
        }

        public override async Task OnConnectedAsync()
        {
            _connections.Add(Context.ConnectionId);
            Console.WriteLine(_connections.Count);
            UpdateAccess();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _connections.Remove(Context.ConnectionId);
            Console.WriteLine(_connections.Count);
            UpdateAccess();
        }

        public async Task AddToAdmin()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId,Admin);
            _connections.Remove(Context.ConnectionId);
            UpdateAccess();
        }

        private void UpdateAccess()
        {
            Clients.Group(Admin).SendAsync("updateAccess", _connections.Count);
        }
    }
}