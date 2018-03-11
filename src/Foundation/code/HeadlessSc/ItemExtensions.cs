using System.Collections.Generic;
using System.Linq;

using HeadlessSc.Pipelines.GetFieldModel;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Pipelines;

namespace HeadlessSc
{
    public static class ItemExtensions
    {
        public static Dictionary<string, object> ToFieldsDictionary(this Item item)
        {
            var fields = item?.Fields
                .Select(f => new KeyValuePair<string, object>(f.Name.ToJsPropertyName(), BuildFieldModel(f)))
                .Where(kv => kv.Value != null)
                .ToArray();
            if (fields == null || fields.Length < 1)
                return null;
            var result = new Dictionary<string, object>();
            foreach (var field in fields)
            {
                if(!result.ContainsKey(field.Key))
                    result.Add(field.Key, field.Value);
            }

            return result;
        }

        private static object BuildFieldModel(Field field)
        {
            var args = new GetFieldModelArgs(field);
            CorePipeline.Run("headless.getFieldModel", args, false);
            return args.Result;
        }
    }
}