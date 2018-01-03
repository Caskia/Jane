using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jane.Reflection
{
    public class AssemblyFinder : IAssemblyFinder
    {
        private readonly IList<Assembly> _assemblies;

        public AssemblyFinder(IList<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        public List<Assembly> GetAllAssemblies()
        {
            return _assemblies.Distinct().ToList();
        }
    }
}