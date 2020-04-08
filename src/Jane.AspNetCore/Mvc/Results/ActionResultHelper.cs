using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Jane.AspNetCore.Mvc.Results
{
    public static class ActionResultHelper
    {
        static ActionResultHelper()
        {
            ObjectResultTypes = new List<Type>
            {
                typeof(JsonResult),
                typeof(ObjectResult),
                typeof(StatusCodeResult)
            };
        }

        public static List<Type> ObjectResultTypes { get; }

        public static bool IsObjectResult(Type returnType)
        {
            //Get the actual return type (unwrap Task)
            if (returnType == typeof(Task))
            {
                returnType = typeof(void);
            }
            else if (returnType.GetTypeInfo().IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                returnType = returnType.GenericTypeArguments[0];
            }

            if (!typeof(IActionResult).IsAssignableFrom(returnType))
            {
                return true;
            }

            return ObjectResultTypes.Any(t => t.IsAssignableFrom(returnType));
        }
    }
}