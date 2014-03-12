using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicIoc
{
    public class MagicLocator
    {
		//ninject kernel
        private Dictionary<string,object> container;

        public MagicLocator()
		{
		    container = new Dictionary<string, object>();
		}

        /// <summary>
        /// Adds the new container.
        /// </summary>
        /// <param name="newContainer">The new container.</param>
        internal void AddNewContainer(Dictionary<string, object> newContainer)
        {
            foreach (KeyValuePair<string, object> dependency in newContainer)
            {
                container.Add(dependency.Key, dependency.Value);
            }
        }


		/// <summary>
		/// Gets the service.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T GetService<T>() where T : class
		{
		    T instance = null;
            //if we've got no container or the assembly isn't loaded
            if (!container.ContainsKey(typeof (T).ToString()))
            {
                //We don't need to lock, it will be done in CacheLoader
                LoadContainer(typeof (T));
            }

            //Now, it's time to try to get the instance
            if (container.ContainsKey(typeof (T).ToString()))
            {
                object result = container[typeof (T).ToString()];
                if (result is T)
                {
                    instance = result as T;
                }
            }
            return instance;

		}

        /// <summary>
        /// Loads the container.
        /// </summary>
        /// <param name="type">The type.</param>
        private void LoadContainer(Type type)
        {
            string[] fullnames = type.FullName.Split('.');
            if (fullnames.Length > 2)
            {
                DependencyResolver.LoadModules(string.Format("{0}.{1}", fullnames[0], fullnames[1]));
            }
        }
    }
}
