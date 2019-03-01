using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRWebPack.Core
{
    class Key : IComparable
    {
        public readonly string user;
        public readonly DateTime dateTime;
        public readonly string Text;

        public Key(Message message)
        {
            this.user = message.User;
            this.dateTime = message.DateTime;
            this.Text = message.Text;
        }

        public int CompareTo(object obj)
        {
            var item = obj as Key;
            if (item == null) return -1;
            var res = this.Text.CompareTo(item.Text);
            if (res == 0) return -1;
            return res;
        }


        public override bool Equals(object obj)
        {
            var key = obj as Key;
            return key != null &&
                   user == key.user &&
                   dateTime == key.dateTime;
        }

        public override int GetHashCode()
        {
            var hashCode = 943605874;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(user);
            hashCode = hashCode * -1521134295 + dateTime.GetHashCode();
            return hashCode;
        }
    }

    public class MessageSortedList
    {
       
        public MessageSortedList()
        {
            data = new SortedList<Key, Message>();
            lockObj = new object();
        }
        private readonly SortedList<Key, Message> data;
        private readonly object lockObj;

        public void Add(Message message)
        {
            lock (lockObj)
            {
                try
                {
                    var key = new Key(message);
                    data.Add(key, message);
                }catch(Exception e)
                {
                    var a = 1;
                }
                
            }
        }

        public IEnumerable<Message> GetAll()
        {
            lock (lockObj)
            {
                return data.Values;
            }
        }
    }
}
