using System;
using System.Text;
using System.Web;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data.ItemResolvers;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.HttpRequest;

namespace HeadlessSc.Abstractions
{
    public class SitecoreContextWrapper : ISitecoreContext
    {
        private ItemPathResolver _pathResolver;

        protected ItemPathResolver PathResolver
        {
            get
            {
                if (_pathResolver != null) return _pathResolver;
                var defaultResolver = new ContentItemPathResolver();
                _pathResolver =
                    (Settings.ItemResolving.FindBestMatch & MixedItemNameResolvingMode.Enabled) ==
                    MixedItemNameResolvingMode.Enabled
                        ? (ItemPathResolver) new MixedItemNameResolver(defaultResolver)
                        : defaultResolver;

                return _pathResolver;
            }
            set => _pathResolver = value;
        }

        public bool TryResolveContextItemByPath(string itemPath)
        {
            if (Context.Database == null || Context.Site == null)
                return false;
            Profiler.StartOperation("Resolve current item.");
            try
            {
                Item item = null;
                var startItem = Context.Database.GetItem(Context.Site.StartPath);
                if (string.IsNullOrEmpty(itemPath) || itemPath == "/")
                    Context.Item = startItem ?? Context.Item;
                var rootItem = Context.Database.GetItem(Context.Site.RootPath);
                itemPath = MainUtil.DecodeName(itemPath);
                if (startItem != null)
                    item = PathResolver.ResolveItem(itemPath, startItem) ??
                           GetItemWithWildcardPaths(startItem, itemPath);
                if (item == null && rootItem != null)
                    item = PathResolver.ResolveItem(itemPath, rootItem) ??
                           GetItemWithWildcardPaths(rootItem, itemPath);



                Context.Item = item ?? Context.Item;
                return item == null;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                Profiler.EndOperation();
            }
        }

        private Item GetItemWithWildcardPaths(Item startItem, string path)
        {
            const char separator = '/';
            Item result = null;
            var parts = path.Split(new[] {separator}, 2, StringSplitOptions.RemoveEmptyEntries);
            var item = PathResolver.ResolveItem(parts[0], startItem) 
                ?? PathResolver.ResolveItem("*", startItem);
            if (item == null)
                return null;
            var isWildcardItem = item.Name == "*";
            if (parts.Length > 1)
                result = GetItemWithWildcardPaths(item, parts[1]);
            else
                return item;
            if (result == null 
                && !isWildcardItem 
                && parts.Length > 1 
                && (item = PathResolver.ResolveItem("*", startItem)) != null)
            {
                result = GetItemWithWildcardPaths(item, parts[1]);
            }
            return result;
        }
    }
}