using Jane.Dependency;
using Jane.Runtime.Session;
using System.Collections.Generic;

namespace Jane.Application.Services
{
    public abstract class ApplicationService : IApplicationService, INeedValidationService, IAvoidDuplicateCrossCuttingConcerns
    {
        #region Fields

        public List<string> AppliedCrossCuttingConcerns { get; } = new List<string>();

        public IJaneSession JaneSession { get; set; }

        #endregion Fields

        public ApplicationService()
        {
            JaneSession = ObjectContainer.Resolve<IJaneSession>();
        }
    }
}