using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace SignalRWebPack.Core.Rep
{
    public class FileMessageRep : IMessageRep, ISaveChanges
    {
        readonly string fileName;
        readonly MemoryMessageRep memoryMessageRep;
        private object lockObj = new object();
        public FileMessageRep(string fileName)
        {
            this.memoryMessageRep = new MemoryMessageRep();
            this.fileName = fileName;
        }
        public IEnumerable<Message> GetAll()
        {
            return memoryMessageRep.GetAll();
        }

        public void Add(Message message)
        {
            memoryMessageRep.Add(message);
        }

        public bool SaveChanges()
        {
            var messages = GetAll().Select(x=>x.ToString());
            lock (lockObj)
            {
                try
                {
                    File.WriteAllLines(fileName, messages);
                    return true;
                }
                catch(Exception e)
                {
                    return false;
                }
            }
        }
    }
}
