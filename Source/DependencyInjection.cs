using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public class DependencyGroupInstaller
    {
        private readonly Assembly assembly;
        private readonly DependencyGroup group;

        public DependencyGroupInstaller(Assembly assembly, DependencyGroup group)
        {
            this.assembly = assembly;
            this.group = group;
        }

        public void Install(IServiceCollection serviceCollection)
        {
            var servicesTypes = CollectServices(assembly, group).ToList();

            foreach (var (attr, serviceType) in servicesTypes)
            {
                if (attr.InterfaceType == null)
                    serviceCollection.Add(new ServiceDescriptor(serviceType, serviceType, attr.ServiceLifetime));
                else
                    serviceCollection.Add(new ServiceDescriptor(attr.InterfaceType, serviceType, attr.ServiceLifetime));
            }
        }

        private static IEnumerable<(RegisterComponentAttribute, Type)> CollectServices(Assembly assembly, DependencyGroup group)
        {
            foreach (var type in assembly.GetTypes())
            {
                var attr = type.GetCustomAttribute<RegisterComponentAttribute>();
                if (attr != null && attr.Group == group)
                    yield return (attr, type);
            }
        }
    }

    public static class DependencyGroupInstallerExtensions
    {
        public static void Install(this IServiceCollection serviceCollection, Assembly assembly, DependencyGroup group)
        {
            var installer = new DependencyGroupInstaller(assembly, group);
            installer.Install(serviceCollection);
        }
    }
}
