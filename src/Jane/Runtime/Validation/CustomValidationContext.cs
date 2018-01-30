using Jane.Dependency;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jane.Runtime.Validation
{
    public class CustomValidationContext
    {
        public CustomValidationContext(List<ValidationResult> results, IObjectContainer container)
        {
            Results = results;
            Container = container;
        }

        /// <summary>
        /// Can be used to resolve dependencies on validation.
        /// </summary>
        public IObjectContainer Container { get; }

        /// <summary>
        /// List of validation results (errors). Add validation errors to this list.
        /// </summary>
        public List<ValidationResult> Results { get; }
    }
}