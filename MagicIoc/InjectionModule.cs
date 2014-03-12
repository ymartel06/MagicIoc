using System;

namespace MagicIoc
{
    /// <summary>
    /// Class to map Interface to Class
    /// </summary>
    public class InjectionModule
    {
        public Type Service { get; set; }
        public Type Implementation { get; set; }

        public override string ToString()
        {
            return Service != null ? Service.ToString() : base.ToString();
        }
    }
}
