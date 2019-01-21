using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Jane.AspNetCore.Mvc.ModelBinding
{
    public class PascalKeyDictionaryBinder<TValue> : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var streamReader = new StreamReader(bindingContext.HttpContext.Request.Body);
            var json = await streamReader.ReadToEndAsync();
            var dictionary = JsonConvert.DeserializeObject<IDictionary<string, TValue>>(json);
            var model = dictionary.Select(keyValuePair =>
            {
                var keyStrs = keyValuePair.Key.Split('_').Select(s => char.ToUpper(s[0]) + s.Substring(1)).ToArray();
                var key = string.Join("", keyStrs);
                return new KeyValuePair<string, TValue>(key, keyValuePair.Value);
            }).ToDictionary(k => k.Key, v => v.Value);

            bindingContext.Result = ModelBindingResult.Success(model);
        }
    }
}