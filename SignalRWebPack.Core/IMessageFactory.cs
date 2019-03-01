using System;

namespace SignalRWebPack.Core
{
    public interface IMessageFactory
    {
        Message CreateMessage(string message, string user, DateTime dateTime);
    }
}