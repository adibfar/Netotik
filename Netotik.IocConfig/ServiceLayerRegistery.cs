using Netotik.Services.Abstract;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Netotik.IocConfig
{
    public class ServiceLayerRegistery : Registry
    {
        public ServiceLayerRegistery()
        {

            Scan(scanner =>
            {
                scanner.WithDefaultConventions();
                scanner.AssemblyContainingType<ICityService>();
            });
        }
    }
}
