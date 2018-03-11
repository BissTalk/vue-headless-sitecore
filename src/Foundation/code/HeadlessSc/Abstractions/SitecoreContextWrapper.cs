using System;
using Sitecore;

namespace HeadlessSc.Abstractions
{
    public class SitecoreContextWrapper : ISitecoreContext
    {
        public bool TryResolveContextItemByPath(string itemPath)
        {
            if (Context.Database == null || Context.Site == null)
                return false;
            try
            {
                if (itemPath == "/")
                    itemPath = string.Empty;
                var fullPath = $"{Context.Site.StartPath.TrimEnd('/')}{itemPath}";
                var item = Context.Database.GetItem(fullPath);
                Context.Item = item ?? Context.Item;
                return item == null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}