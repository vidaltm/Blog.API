using Blog.Api.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Api.Services
{
    public class MessageHub : Hub<IMessageHubClient>
    {
        public async Task SendMessageToHub(string message)
        {
            await Clients.All.SendMessageToHub(message);
        }
    }
}
