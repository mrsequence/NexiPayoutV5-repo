using NexiPayout.Framework.Interfaces;
using Ninject;
using Ninject.Modules;
using System.Reflection;

namespace NexiPayout.Framework.Logging
{
    public class NinjectBinding
        : NinjectModule
    {
        public static ILogger PayoutLogger;
        public override void Load()
        {
            Bind<ILogger>().To<SerilogAdapter>();
        }

        public static void SetLogger()
        {
            IKernel Kernel = new StandardKernel();
            Kernel.Load(Assembly.GetExecutingAssembly());
            PayoutLogger = Kernel.Get<ILogger>();
        }
    }
}
