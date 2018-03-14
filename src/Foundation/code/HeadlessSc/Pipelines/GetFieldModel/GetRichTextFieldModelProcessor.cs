using System.Globalization;
using System.Web.Mvc;
using Sitecore.Data.Fields;
using Sitecore.Mvc;
using Sitecore.Mvc.Presentation;
using Field = HeadlessSc.Areas.Headless.Models.Field;

namespace HeadlessSc.Pipelines.GetFieldModel
{
    public class GetRichTextFieldModelProcessor : IHeadlessGetFieldModelPipeline
    {
        public void Process(GetFieldModelArgs args)
        {
            if (args.Result != null || args.Field.Name.StartsWith("_", true, CultureInfo.InvariantCulture) ||
                !args.Field.HasValue)
                return;

            if (!(FieldTypeManager.GetField(args.Field) is HtmlField))
                return;

            args.Result = new Field
            {
                FieldType = args.Field.Type,
                Value = new HtmlHelper(new HeadlessViewContext(), new ViewDataContainer(new ViewDataDictionary())) 
                    .Sitecore().Field(args.Field.Name, args.Field.Item).ToHtmlString()
            };
        }
    }
}