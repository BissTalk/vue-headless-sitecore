using Sitecore.Data.Fields;
using Sitecore.Links;

namespace HeadlessSc.Pipelines.GetFieldModel
{
    public class GetReferenceOrLookupFieldModelProcessor : IHeadlessGetFieldModelPipeline
    {
        public void Process(GetFieldModelArgs args)
        {
            if (args.RecursionLevel > 5 || args.Result != null || !args.Field.HasValue)
                return;
            var lookupField = FieldTypeManager.GetField(args.Field) as LookupField;
            var itemField = FieldTypeManager.GetField(args.Field) as ReferenceField;

            if (lookupField == null && itemField == null) 
                return;

            var item = itemField?.TargetItem ?? lookupField?.TargetItem;
            if (item == null)
            {
                args.AbortPipeline();
                return;
            }
            var dict = item.ToFieldsDictionary(args.RecursionLevel + 1);
            var path = LinkManager.GetItemUrl(item, new UrlOptions
            {
                AddAspxExtension = false,
                AlwaysIncludeServerUrl = false,
                EncodeNames = true,
                LanguageEmbedding = LanguageEmbedding.Never,
                LowercaseUrls = true
            });

            args.Result = new {name = item.Name, displayName = item.DisplayName, path, fields = dict};
        }
    }
}