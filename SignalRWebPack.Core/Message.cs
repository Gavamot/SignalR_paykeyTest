using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRWebPack.Core
{
    public class Message 
    {
        internal Message() { }
        public string User { get; set; }  
        public DateTime DateTime { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return $"{User}|{DateTime}|{Text}";
        }
    }
}
