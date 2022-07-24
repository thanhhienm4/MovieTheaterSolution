using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace MovieTheater.BackEnd.Hub
{
    public class ReservationHub :Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task EnableSeat(int screeningId, int seatId)
        {
            await Clients.OthersInGroup(screeningId.ToString()).SendAsync("EnableSeat", seatId);
        }

        public async Task DisableSeat(int screeningId, int seatId)
        {
            await Clients.OthersInGroup(screeningId.ToString()).SendAsync("DisableSeat", seatId);
        }
        public async Task AddToGroup(int screeningId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, screeningId.ToString());
        }

        public async Task RemoveFromGroup(int screeningId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, screeningId.ToString());
        }
    }
}
