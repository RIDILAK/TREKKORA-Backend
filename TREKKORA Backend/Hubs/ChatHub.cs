using Microsoft.AspNetCore.SignalR;

namespace TREKKORA_Backend.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessage(Guid senderId,Guid recieverId,string message)
        {
            await Clients.Users(recieverId.ToString()).SendAsync("recieveMessage",senderId,message);
        }
    }
}
