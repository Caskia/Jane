using Autofac;
using ENode.Configurations;

namespace JaneENodeGenericHostExample
{
    public class ENodeManager
    {
        public static ENodeConfiguration Configuration { get; set; }

        public static ContainerBuilder ContainerBuilder { get; set; }
    }
}