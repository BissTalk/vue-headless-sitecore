using System;
using Sitecore.Pipelines;
using Sitecore.Sites;

namespace HeadlessSc.Pipelines.GetRenderings
{
    /// <summary>
    ///     Sets up the requried context to allow the code to leverage the existing MVC pipelines.
    /// </summary>
    /// <seealso cref="IHeadlessGetRenderingsProcessor" />
    public class ResolveHeadlessContextProcessor : IHeadlessGetRenderingsProcessor
    {
        /// <summary>
        ///     Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Process(GetRenderingsArgs args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));

            Sitecore.Context.Site.SetDisplayMode(DisplayMode.Normal, DisplayModeDuration.ResetAfterRequest);
            CorePipeline.Run("mvc.requestBegin", args);
            if (args.PageContext != null)
                args.PageContext.Item = Sitecore.Context.Item ?? args.PageContext.Item;

        }
    }
}