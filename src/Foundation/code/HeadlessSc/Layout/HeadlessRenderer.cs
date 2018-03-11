using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HeadlessSc.Areas.Headless.Models;
using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Presentation;

namespace HeadlessSc.Layout
{
    public class HeadlessRenderer : Renderer
    {
        private readonly Rendering _rendering;

        public HeadlessRenderer(Rendering rendering)
        {
            _rendering = rendering;
        }

        public override void Render(TextWriter writer)
        {
            if (!(writer is HeadlessResponseWriter headless))
                return;
            var item = _rendering.Item != Sitecore.Context.Item
                ? _rendering.Item
                : null;
            headless.Result = new ComponentItem
            {
                ComponentName = _rendering.RenderingItem.Name.Replace(" ", String.Empty),
                Name = item?.Name,
                DisplayName = item?.DisplayName,
                Fields = item?.ToFieldsDictionary(),
                PlaceholderName = _rendering.Placeholder,
                Parameters = _rendering.Parameters.Any()
                    ? new Dictionary<string, string>()
                    : null
            };
            foreach (var keyValuePair in _rendering.Parameters)
                headless.Result.Parameters.Add(keyValuePair.Key.ToJsPropertyName(), keyValuePair.Value);
        }
    }
}