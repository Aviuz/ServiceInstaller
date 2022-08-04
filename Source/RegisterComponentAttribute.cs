using System;

namespace Microsoft.Extensions.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RegisterComponentAttribute : Attribute
    {
        public DependencyGroup Group { get; }
        public Type InterfaceType { get; set; } = null;
        public ServiceLifetime ServiceLifetime { get; set; }

        public RegisterComponentAttribute(DependencyGroup group, ServiceLifetime serviceLifetime)
        {
            Group = group;
            ServiceLifetime = serviceLifetime;
        }

        public RegisterComponentAttribute(DependencyGroup group, Type interfaceType, ServiceLifetime serviceLifetime)
        {
            Group = group;
            InterfaceType = interfaceType;
            ServiceLifetime = serviceLifetime;
        }
    }
}
