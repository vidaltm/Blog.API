using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Api.Services.Interfaces
{
    public interface IMessageHubClient
    {
        Task SendMessageToHub(string message);
    }
}
