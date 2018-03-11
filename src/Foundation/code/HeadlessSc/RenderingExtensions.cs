using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Mvc.Presentation;

namespace HeadlessSc
{
    public static class RenderingExtensions
    {
        public static bool IsLayoutRendering(this Rendering rendering)
        {
            return rendering.RenderingType == "Layout";
        }
    }
}