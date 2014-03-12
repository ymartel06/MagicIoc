using System.Collections.Generic;

namespace MagicIoc.SimpleExample
{
    public class MagicModule
    {
        /// <summary>
        /// Gets the injection modules.
        /// </summary>
        /// <returns></returns>
        public static List<InjectionModule> GetInjectionModules()
        {
            List<InjectionModule> registrations = new List<InjectionModule>();
            InjectionModule injectionModule = new InjectionModule
                {
                    Service = typeof (IMyService),
                    Implementation = typeof (MyService)
                };
            registrations.Add(injectionModule);
            return registrations;
        }
    }
}
