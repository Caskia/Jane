using Jane.Timing;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Jane.AspNetCore.Mvc.ModelBinding
{
    public class JaneDateTimeModelBinder : IModelBinder
    {
        private readonly SimpleTypeModelBinder _simpleTypeModelBinder;
        private readonly Type _type;

        public JaneDateTimeModelBinder(Type type, ILoggerFactory loggerFactory)
        {
            _type = type;
            _simpleTypeModelBinder = new SimpleTypeModelBinder(type, loggerFactory);
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            await _simpleTypeModelBinder.BindModelAsync(bindingContext);

            if (!bindingContext.Result.IsModelSet)
            {
                return;
            }

            if (_type == typeof(DateTime))
            {
                var dateTime = (DateTime)bindingContext.Result.Model;
                bindingContext.Result = ModelBindingResult.Success(Clock.Normalize(dateTime));
            }
            else
            {
                var dateTime = (DateTime?)bindingContext.Result.Model;
                if (dateTime != null)
                {
                    bindingContext.Result = ModelBindingResult.Success(Clock.Normalize(dateTime.Value));
                }
            }
        }
    }
}