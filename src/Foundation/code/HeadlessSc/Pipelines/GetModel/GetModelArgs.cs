using System;
using System.Collections.Generic;
using HeadlessSc.Areas.Headless.Models;
using Sitecore.Mvc.Pipelines;
using Sitecore.Mvc.Presentation;

namespace HeadlessSc.Pipelines.GetModel
{
    public class GetModelArgs : MvcPipelineArgs
    {
        public GetModelArgs(IEnumerable<Rendering> renderings)
        {
            Renderings = renderings ?? throw new ArgumentNullException(nameof(renderings));
        }

        public IEnumerable<Rendering> Renderings { get; }

        internal IDictionary<string, ComponentItem> Placeholders { get; } = new Dictionary<string, ComponentItem>();

        public object Result { get; set; }
    }
}