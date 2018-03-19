using Sitecore.Data.Fields;

namespace HeadlessSc.Pipelines.GetFieldModel
{
    public class GetNameValuePairFieldModelProcessor : IHeadlessGetFieldModelPipeline
    {
        public void Process(GetFieldModelArgs args)
        {
            if (args.Result != null || !args.Field.HasValue)
                return;

            if (!(FieldTypeManager.GetField(args.Field) is NameValueListField valueListField))
                return;

            args.Result = new
            {
                values = valueListField.NameValues,
                type = valueListField.InnerField.Type
            };
        }
    }
}