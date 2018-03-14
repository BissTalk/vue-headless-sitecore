using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Sitecore;
using Sitecore.Data.Fields;
using Sitecore.Resources.Media;

namespace HeadlessSc.Pipelines.GetFieldModel
{
    public class GetImageFieldModelProcessor : IHeadlessGetFieldModelPipeline
    {
        public void Process(GetFieldModelArgs args)
        {
            if (args.Result != null || args.Field.Name.StartsWith("_", true, CultureInfo.InvariantCulture) || !args.Field.HasValue)
                return;

            if (!(FieldTypeManager.GetField(args.Field) is ImageField imagefield))
                return;

            args.Result = new
            {
                @class = imagefield.Class.ToJsonValueString(),
                alt = imagefield.Alt.ToJsonValueString(),
                src = imagefield.MediaItem != null
                ?  MediaManager.GetMediaUrl(imagefield.MediaItem, new MediaUrlOptions
                    {
                        AlwaysIncludeServerUrl= false,
                        IncludeExtension = true,
                        LowercaseUrls = true,
                        
                    })
                : null,
                linkType = imagefield.LinkType.ToJsonValueString(),
                border = imagefield.Border.ToJsonValueString(),
                hspace = imagefield.HSpace.ToJsonValueString(),
                height = imagefield.Height.ToJsonValueString(),
                width = imagefield.Width.ToJsonValueString(),
                type = imagefield.InnerField.Type
            };
        }
    }
}