using System.Globalization;
using HeadlessSc.Areas.Headless.Models;

namespace HeadlessSc.Pipelines.GetFieldModel
{
    public class GetDefaultFieldModelProcessor : IHeadlessGetFieldModelPipeline
    {
        public void Process(GetFieldModelArgs args)
        {
            if (args.Result != null || args.Field.Name.StartsWith("_", true, CultureInfo.InvariantCulture) || !args.Field.HasValue)
                return;
            args.Result = new Field {FieldType = args.Field.Type, Value = args.Field.Value};
        }
    }
}