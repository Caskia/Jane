using Autofac;
using ENode.Configurations;

namespace JaneENodeGenericHostBuilderExample
{
    public class ENodeManager
    {
        public static ENodeConfiguration Configuration { get; set; }

        public static ContainerBuilder ContainerBuilder { get; set; }
    }
}