using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HeadlessSc.Areas.Headless.Models;
using HeadlessSc.Layout;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Web;

namespace HeadlessSc.Pipelines.GetModel
{
    public class GetModelProcessor : IHeadlessGetModelPipeline
    {
        private static IComparer<ComponentItem> EvaluationOrder { get; } = new ComponentEvaluationSorter();

        private static IComparer<ComponentItem> FinalOrder { get; } = new ComponentSorter();

        public void Process(GetModelArgs args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));

            if (args.Result != null)
                return;

            var result = CreateResultItem(args.PageContext.Item);
            SetupPlaceholders(result);
            AddRenderingsToResult(result, args.Renderings);
            args.Result = result;
        }

        private static void AddRenderingsToResult(ItemModel model, IEnumerable<Rendering> renderings)
        {
            if (renderings == null)
                return;
            var components = CreateComponentItems(renderings);

            components.Sort(EvaluationOrder);
            foreach (var component in components)
            {
                var key = component.PlaceholderName.TrimStart('/');
                if (TryAddComponentToLayoutPlaceholder(model, key, component)) continue;
                if (TryAddComponentToParentComponentByName(components, key, component)) continue;

                TryAddComponentToParentComponentByPath(model, key, component);
            }

            FixComponentSortOrder(model);
        }

        private static bool TryAddComponentToParentComponentByPath(ItemModel model, string key, ComponentItem component)
        {
            var keys = key.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);

            var currentPlaceholder =
                model.Placeholders.ContainsKey(keys[0])
                    ? model.Placeholders[keys[0]]
                    : null;

            var i = 1;
            while (currentPlaceholder != null && i < keys.Length)
            {
                currentPlaceholder =
                    currentPlaceholder.FirstOrDefault(c => c.Placeholders?.ContainsKey(keys[i]) ?? false)
                        ?.Placeholders[keys[i]];
                i++;
            }

            currentPlaceholder?.Add(component);
            return currentPlaceholder != null;
        }

        private static bool TryAddComponentToParentComponentByName(List<ComponentItem> components, string key,
            ComponentItem component)
        {
            if (!key.Contains("/"))
            {
                var parent = components.FirstOrDefault(c => c.Placeholders?.ContainsKey(key) ?? false);
                parent?.Placeholders[key].Add(component);
                return true;
            }

            return false;
        }

        private static bool TryAddComponentToLayoutPlaceholder(ItemModel model, string key, ComponentItem component)
        {
            if (model.Placeholders.ContainsKey(key))
            {
                model.Placeholders[key].Add(component);
                return true;
            }

            return false;
        }

        private static List<ComponentItem> CreateComponentItems(IEnumerable<Rendering> renderings)
        {
            var renderingList = renderings.ToList();
            var components = new List<ComponentItem>();

            foreach (var rendering in renderingList)
            {
                var component = BuildComponent(rendering, components);
                if (component != null)
                    components.Add(component);
            }

            return components;
        }

        private static ComponentItem BuildComponent(Rendering rendering, List<ComponentItem> components)
        {
            var sr = new HeadlessResponseWriter();
            rendering.Renderer.Render(sr);
            if (sr.Result == null)
                return null;
            sr.Result.Sort = components.Count;
            MultilistField f =
                rendering.RenderingItem.InnerItem.Fields[new ID("{069A8361-B1CD-437C-8C32-A3BE78941446}")];
            foreach (var itemName in f.GetItems().Select(i => i.Name).Distinct())
            {
                if (sr.Result.Placeholders == null)
                    sr.Result.Placeholders = new Dictionary<string, List<ComponentItem>>();
                sr.Result.Placeholders.Add(itemName, new List<ComponentItem>());
            }

            return sr.Result;
        }

        private static void FixComponentSortOrder(ItemModel model)
        {
            if (model?.Placeholders == null)
                return;

            foreach (var components in model.Placeholders.Values)
            {
                components.Sort(FinalOrder);
                foreach (var component in components)
                    FixComponentSortOrder(component);
            }
        }

        private static LayoutItem GetQueryStringLayout()
        {
            var queryString = WebUtil.GetQueryString("sc_layout");
            if (string.IsNullOrEmpty(queryString))
                return null;
            return Context.Database.GetItem(queryString);
        }

        private static void SetupPlaceholders(ItemModel model)
        {
            var layout = GetQueryStringLayout() ?? Context.Item.Visualization.Layout;
            if (layout == null)
                return;

            MultilistField f =
                layout.InnerItem.Fields[new ID("{80334869-86DC-4472-AA89-44CF1B2F6C9B}")];
            foreach (var itemName in f.GetItems().Select(i => i.Name).Distinct())
            {
                if (model.Placeholders == null)
                    model.Placeholders = new Dictionary<string, List<ComponentItem>>();
                model.Placeholders.Add(itemName, new List<ComponentItem>());
            }
        }

        private static ItemModel CreateResultItem(Item item)
        {
            return new ItemModel
            {
                Name = item?.Name,
                DisplayName = item?.DisplayName,
                Fields = item.ToFieldsDictionary()
            };
        }

        private class ComponentSorter : IComparer<ComponentItem>
        {
            public int Compare(ComponentItem x, ComponentItem y)
            {
                if (x == null) throw new ArgumentNullException(nameof(x));
                if (y == null) throw new ArgumentNullException(nameof(y));

                return x.Sort.CompareTo(y.Sort);
            }
        }

        private class ComponentEvaluationSorter : IComparer<ComponentItem>
        {
            public int Compare(ComponentItem x, ComponentItem y)
            {
                if (x == null) throw new ArgumentNullException(nameof(x));
                if (y == null) throw new ArgumentNullException(nameof(y));

                if (x.PlaceholderName.StartsWith("/") && y.PlaceholderName.StartsWith("/")
                    || !x.PlaceholderName.StartsWith("/") && !y.PlaceholderName.StartsWith("/"))
                    return string.Compare(x.PlaceholderName, y.PlaceholderName, true, CultureInfo.InvariantCulture);
                return x.PlaceholderName.StartsWith("/")
                    ? 1
                    : -1;
            }
        }
    }
}