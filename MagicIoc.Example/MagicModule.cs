using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MagicIoc.Example
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
