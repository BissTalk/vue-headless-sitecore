using System.Globalization;

namespace HeadlessSc.Pipelines.GetFieldModel
{
    public class FieldModelFilterProcessor : IHeadlessGetFieldModelPipeline
    {
        public void Process(GetFieldModelArgs args)
        {
            if (args.Field.Name.StartsWith("_", true, CultureInfo.InvariantCulture) || !args.Field.HasValue)
                args.AbortPipeline();
        }
    }
}