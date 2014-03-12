MagicIoc
========

A C# IOC based on naming convention, nothing to configure!

You have just to fallow some rules, and the IOC will work.

How to do
=========

1) Your assembly where you have Interfaces and Implementations have to be named by only 2 words.

Example: MyCompany.Core -> It will result MyCompany.Core.dll 

2) Add an injection modules

Add a MagicModule class at the root of your assembly. The method must be named GetInjectionModules and the class MagicModule.

Example:

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

	
3) That's all!

You can try it like that:

	DependencyResolver.ServiceLocator.GetService<IMyService>().Test();
