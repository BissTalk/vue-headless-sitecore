using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeadlessSc.Abstractions;
using Sitecore.Pipelines.HttpRequest;

namespace HeadlessSc.Pipelines
{
    public class HeadlessItemContextResolver: HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            if (args.Context.IsHeadless())
                ProcessHeadless(new HttpContextWrapper(args.Context), new SitecoreContextWrapper());
        }

        public void ProcessHeadless(HttpContextBase httpContext, ISitecoreContext sitecoreContext)
        {
            if(httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            if(sitecoreContext == null) throw new ArgumentNullException(nameof(sitecoreContext));

            if (!httpContext.IsHeadless()) return;

            var path = httpContext.GetQueryStringValue("path");

            sitecoreContext.TryResolveContextItemByPath(path);
        }
    }
}