using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MagicIoc
{
    public static class DependencyResolver
    {
        private const string MagicInjectionClassName = "MagicModule";
        private const string GetInjectionModulesMethod = "GetInjectionModules";

        private static volatile MagicLocator serviceLocator = new MagicLocator();
        private static readonly object cacheLoaderLock = new object();

        private static Dictionary<string, bool> loadedCaches = new Dictionary<string, bool>(); 

        public static MagicLocator ServiceLocator
        {
            get
            {
                return serviceLocator;
            }
            set { serviceLocator = value; }
        }

        private static Dictionary<string, object> InjectionModulesToContainer(IEnumerable<InjectionModule> injectionModules)
        {
            Dictionary<string, object> container = new Dictionary<string, object>();
            foreach (InjectionModule injectionModule in injectionModules)
            {
                container.Add(injectionModule.Service.ToString(), Activator.CreateInstance(injectionModule.Implementation));
            }
            return container;
        }


        private static bool AlreadyLoaded(string assemblyName)
        {
            return (loadedCaches.ContainsKey(assemblyName)) && (loadedCaches[assemblyName]);
        }


        internal static void LoadModules(string assemblyName)
        {
            if (!AlreadyLoaded(assemblyName))
            {
                lock (cacheLoaderLock)
                {
                    if (!AlreadyLoaded(assemblyName))
                    {
                        //find a list of injection modules
                        IEnumerable<InjectionModule> injectionModules = LoadCacheModules(assemblyName, String.Format("{0}.{1}", assemblyName, MagicInjectionClassName), GetInjectionModulesMethod);

                        //init the module and put it inside the locator
                        serviceLocator.AddNewContainer(InjectionModulesToContainer(injectionModules));

                        //set a flag to already loaded
                        loadedCaches.Add(assemblyName,true);
                    }
                }
            }
        }

        /// <summary>
        /// Loads the cache modules thanks to reflection on the assembly.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns></returns>
        private static IEnumerable<InjectionModule> LoadCacheModules(string assemblyName, string className, string methodName)
        {
            List<InjectionModule> injectionModules = null;
            assemblyName = assemblyName + ".dll";
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {

                if (assembly.Location.ToLower().IndexOf(assemblyName.ToLower()) > -1)
                {
                    //create an instance
                    Type type = assembly.GetType(className);
                    object instance = Activator.CreateInstance(type);
                    //call the method to get the modules
                    object result = type.InvokeMember(methodName, BindingFlags.Default | BindingFlags.InvokeMethod, null, instance, null);
                    if (result is List<InjectionModule>)
                    {
                        injectionModules = result as List<InjectionModule>;
                    }
                    break;
                }
            }
            return injectionModules;
        }



        /// <summary>
        /// Resets the cache loader and the service locator.
        /// Warning: Don't use this method except in Unit Tests.
        /// If you are doing a reset, you also have to reload the cache thanks to load methods.
        /// </summary>
        public static void Reset()
        {
            lock (cacheLoaderLock)
            {
                //reset the service locator 
                ServiceLocator = new MagicLocator();

                //reset the cache loader state
                loadedCaches = new Dictionary<string, bool>(); 
            }
        }
    }
}
