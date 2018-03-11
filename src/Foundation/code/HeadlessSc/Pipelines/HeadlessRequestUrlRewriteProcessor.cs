using System;
using System.Globalization;
using System.Linq;
using System.Web;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Exceptions;
using Sitecore.Pipelines.PreprocessRequest;

namespace HeadlessSc.Pipelines
{
    /// <summary>
    ///     Pre-processes requests for headless content.
    /// </summary>
    /// <seealso cref="PreprocessRequestProcessor" />
    public class HeadlessRequestUrlRewriteProcessor : PreprocessRequestProcessor
    {

        /// <summary>
        /// Executes the rewrite.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="routePrefix">The route prefix.</param>
        public virtual void ExecuteRewrite([NotNull] HttpContextBase httpContext, [NotNull] string routePrefix)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            if (routePrefix == null) throw new ArgumentNullException(nameof(routePrefix));
            if (httpContext.Request.Url == null)
                throw new RequiredObjectIsNullException("httpContext.Request.Url cannot be null.");

            var localPath = httpContext.Request.Url?.LocalPath;
            if (!IsValidLocalPath(localPath, routePrefix))
                return;
            var pathWithoutPrefix = localPath.Substring(routePrefix.Length);
            if (!TryGetParts(pathWithoutPrefix, out var site, out var path, out var language)) return;
            var newUrl = CreateNewUrl(httpContext, routePrefix, site, path, language);
            httpContext.RewritePath(newUrl);
            httpContext.FlagAsHeadless();
        }

        /// <summary>
        ///     Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public override void Process(PreprocessRequestArgs args)
        {
            ExecuteWithoutException(() =>
            {;
                if (args?.Context?.Request.Path == null ||
                    !args.Context.Request.Path.StartsWith(ModuleInfo.RoutePrefix, false, CultureInfo.InvariantCulture))
                    return;
                ExecuteRewrite(new HttpContextWrapper(args.Context), ModuleInfo.RoutePrefix.TrimEnd('/'));
            });
        }

        /// <summary>
        /// Creates the new URL.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="routePrefix">The route prefix.</param>
        /// <param name="site">The site.</param>
        /// <param name="path">The path.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        private static string CreateNewUrl(HttpContextBase httpContext, string routePrefix, string site, string path,
            string language)
        {
            var newUrl =
                $"{routePrefix}?sc_site={HttpUtility.UrlEncode(site)}&path={HttpUtility.UrlEncode(path)}&sc_lang={HttpUtility.UrlEncode(language)}";
            var query = httpContext.Request.Url?.Query.TrimStart('?', '&');
            if (query?.Length > 0)
                newUrl = string.Concat(newUrl, "&", query);
            return newUrl;
        }

        /// <summary>
        /// Determines whether [is valid local path] [the specified local path].
        /// </summary>
        /// <param name="localPath">The local path.</param>
        /// <param name="routePrefix">The route prefix.</param>
        /// <returns>
        ///   <c>true</c> if [is valid local path] [the specified local path]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsValidLocalPath(string localPath, string routePrefix)
        {
            return localPath.StartsWith(routePrefix, true, CultureInfo.InvariantCulture)
                   && localPath.EndsWith(".json", true, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Tries to get parts of the URL.
        /// </summary>
        /// <param name="pathWithoutPrefix">The path without prefix.</param>
        /// <param name="site">The site.</param>
        /// <param name="path">The path.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        private static bool TryGetParts(string pathWithoutPrefix, out string site, out string path, out string language)
        {
            site = path = language = null;
            var parts = pathWithoutPrefix.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2) return false;
            site = parts[0];
            path = parts.Length > 2
                ? StringUtil.EnsurePrefix('/', string.Join("/", parts.Skip(1).Take(parts.Length - 2)))
                : "/";
            language = parts[parts.Length - 1].Substring(0, parts[parts.Length - 1].Length - 5);
            return true;
        }

        /// <summary>
        /// Executes the without exception.
        /// </summary>
        /// <param name="action">The action.</param>
        private void ExecuteWithoutException([NotNull] Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                Log.Error($"HeadlessRequestUrlRewriteProcessor:{e.Message}", e, this);
            }
        }
    }
}