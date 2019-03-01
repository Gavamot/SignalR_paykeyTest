using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SignalRWebPack.Core.Rep
{
    public class MemoryMessageRep : IMessageRep
    {
        private readonly MessageSortedList data;

        public MemoryMessageRep()
        {
            this.data = new MessageSortedList();
        }

        public IEnumerable<Message> GetAll()
        {
            return data.GetAll();
        } 

        public void Add(Message item)
        {
            data.Add(item);
        }
    }
}
