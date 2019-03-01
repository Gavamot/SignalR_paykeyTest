using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRWebPack.Core
{
    public class MessageFactory : IMessageFactory
    {
        public Message CreateMessage(string message, string user, DateTime dateTime)
        {
            return new Message
            {
                Text = message,
                User = user,
                DateTime = dateTime
            };
        } 
    }
}
