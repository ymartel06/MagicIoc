using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MagicIoc.AutoLoadExample
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
            Type declaringType = MethodBase.GetCurrentMethod().DeclaringType;
            if (declaringType != null)
            {
                //get the current assembly
                var repositoryAssembly = declaringType.Assembly;

                var q = from type in repositoryAssembly.GetExportedTypes()
                        where type.Namespace == "MagicIoc.AutoLoadExample.Cache"
                              && type.GetInterfaces().Any()
                        select new InjectionModule
                        {
                            Service = type.GetInterfaces().Single(),
                            Implementation = type
                        };
                registrations = q.ToList();
            }
            return registrations;
        }
    }
}
