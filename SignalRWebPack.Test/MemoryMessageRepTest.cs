using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SignalRWebPack.Core;
using SignalRWebPack.Core.Rep;

namespace SignalRWebPack.Test
{
    [TestFixture]
    class MemoryMessageRepTest : BaseTest
    {
        public MemoryMessageRep rep;
        public readonly MessageFactory factory = new MessageFactory();

        public readonly DateTime CurrentDateTime = new DateTime(2016, 1, 1);

        [SetUp]
        public void Init()
        {
            rep = new MemoryMessageRep();
        }

        [Test]
        public void MemoryMessageTest_Empty()
        {
            var expected = rep.GetAll();
            var actual = new Message[0];
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MemoryMessageTest_SameMessage()
        {
            var getSame = new Func<Message>(()=> factory.CreateMessage("Message 1", "Vasia", CurrentDateTime));
            rep.Add(getSame());
            rep.Add(getSame());
            rep.Add(getSame());
            var expected = rep.GetAll().ToArray();
            var actual = new []{ getSame(), getSame(), getSame() };
            AreEqual(expected, actual); 
        }

        [Test]
        public void MemoryMessageTest_AddDiferent()
        {
            rep.Add(factory.CreateMessage("Message 1", "Vasia", CurrentDateTime));
            rep.Add(factory.CreateMessage("Message 2", "Vasia 2", CurrentDateTime));
            rep.Add(factory.CreateMessage("Message 3", "Vasia 3", CurrentDateTime));
            var expected = rep.GetAll().ToArray();
            var actual = new[]
            {
                factory.CreateMessage("Message 1", "Vasia", CurrentDateTime),
                factory.CreateMessage("Message 2", "Vasia 2", CurrentDateTime),
                factory.CreateMessage("Message 3", "Vasia 3", CurrentDateTime)
            };
            AreEqual(expected, actual);
        }


    }
}
