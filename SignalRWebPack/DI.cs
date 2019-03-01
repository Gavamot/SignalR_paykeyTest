using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SignalRWebPack.Controllers;
using SignalRWebPack.Core;
using SignalRWebPack.Core.Rep;
using System.Linq;
using System.Reflection;

namespace SignalRWebPack
{
    public static class DI
    {
        static IContainer container { get; set; }

        public static T Resolve<T>()
        {
            var res = container.Resolve<T>();
            return res;
        }

        public static void ConfigureContainer(FileMessageRep fileMessageRep)
        {
            var builder = new ContainerBuilder();

            var rep = new MemoryMessageRep();

            // Базовый бандинг
            var assembly = Assembly.GetAssembly(typeof(MemoryMessageRep));
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();

 
            builder.Register(x => fileMessageRep)
                .As<IMessageRep>()
                .As<ISaveChanges>()
                .SingleInstance();

            //builder.RegisterType<MemoryMessageRep>()
            //    .As<IMessageRep>()
            //    .SingleInstance();

            builder.RegisterType<MessageFactory>()
                .As<IMessageFactory>()
                .SingleInstance();



            container = builder.Build();
        }
    }
}
