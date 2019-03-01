using System;
using System.Collections.Generic;

namespace SignalRWebPack.Core.Rep
{
    public interface IMessageRep
    {
        IEnumerable<Message> GetAll();
        void Add(Message message);
    }
}
