using Jane.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jane.QCloud.Soe
{
    public interface IQCloudSoeService : ISingletonDependency
    {
        Task<EvaluateOutput> EvaluateAsync(EvaluateInput input);
    }
}