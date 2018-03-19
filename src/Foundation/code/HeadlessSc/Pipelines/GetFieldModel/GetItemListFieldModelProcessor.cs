using System.Collections.Generic;
using Sitecore.Data.Fields;
using Sitecore.Links;

namespace HeadlessSc.Pipelines.GetFieldModel
{
    public class GetItemListFieldModelProcessor : IHeadlessGetFieldModelPipeline
    {
        public void Process(GetFieldModelArgs args)
        {
            if (args.RecursionLevel > 5 || args.Result != null || !args.Field.HasValue)
                return;

            if (!(FieldTypeManager.GetField(args.Field) is MultilistField listField))
                return;

            var items = new List<object>();
            foreach (var item in listField.GetItems())
            {
                var dict = item.ToFieldsDictionary(args.RecursionLevel + 1);
                var path = LinkManager.GetItemUrl(item, new UrlOptions
                {
                    AddAspxExtension = false,
                    AlwaysIncludeServerUrl = false,
                    EncodeNames = true,
                    LanguageEmbedding = LanguageEmbedding.Never,
                    LowercaseUrls = true
                });
                items.Add(new {name = item.Name, displayName = item.DisplayName, path, fields = dict});
            }

            args.Result = new {items, type = listField.InnerField.Type}
                ;
        }
    }
}