using Microsoft.AspNetCore.SignalR;
using SignalRWebPack.Core;
using SignalRWebPack.Core.Rep;
using System;
using System.Threading.Tasks;

namespace SignalRWebPack.Hubs
{
    public class ChatHub : Hub
    {
        readonly IMessageRep messageRep = DI.Resolve<IMessageRep>();
        readonly IMessageFactory messageFactory = DI.Resolve<IMessageFactory>();

        public async Task NewMessage(string username, string message)
        {
            if (string.IsNullOrEmpty(message.Trim()))
                return;
            var now = DateTime.Now;
            await Clients.All.SendAsync("messageReceived", username, now, message);
            await AddMessageToRep(message, username, now);
        }

        private async Task AddMessageToRep(string message, string username, DateTime now)
        {
            await Task.Factory.StartNew(() =>
            {
                var msg = messageFactory.CreateMessage(message, username, now);
                messageRep.Add(msg);
            });
        }
    }
}
