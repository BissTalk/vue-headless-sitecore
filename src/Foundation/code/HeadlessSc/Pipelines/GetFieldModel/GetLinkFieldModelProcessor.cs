using Sitecore.Data.Fields;

namespace HeadlessSc.Pipelines.GetFieldModel
{
    public class GetLinkFieldModelProcessor : IHeadlessGetFieldModelPipeline
    {
        public void Process(GetFieldModelArgs args)
        {
            if (args.Result != null || !args.Field.HasValue)
                return;

            if (!(FieldTypeManager.GetField(args.Field) is LinkField linkfield))
                return;

            args.Result = new
            {
                @class = linkfield.Class.ToJsonValueString(),
                anchor = linkfield.Anchor.ToJsonValueString(),
                href = linkfield.GetFriendlyUrl().ToJsonValueString(),
                linkType = linkfield.LinkType.ToJsonValueString(),
                text = linkfield.Text.ToJsonValueString(),
                title = linkfield.Title.ToJsonValueString(),
                target = linkfield.Target.ToJsonValueString(),
                type = linkfield.InnerField.Type
            };
        }
    }
}