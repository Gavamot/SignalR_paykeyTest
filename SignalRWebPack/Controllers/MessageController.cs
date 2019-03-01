using Microsoft.AspNetCore.Mvc;
using SignalRWebPack.Core;
using SignalRWebPack.Core.Rep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRWebPack.Controllers
{
    public class MessageController : Controller
    {
        readonly IMessageRep rep = DI.Resolve<IMessageRep>();

        public IEnumerable<Message> GetAll()
        {
            return rep.GetAll();
        }
    }
}
