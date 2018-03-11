using System;
using Sitecore.Mvc.Presentation;

namespace HeadlessSc.Pipelines.GetRenderings
{
    /// <summary>
    ///     Pipeline processor that resolves all renderings from the <see cref="PageContext" />
    /// </summary>
    /// <seealso cref="IHeadlessGetRenderingsProcessor" />
    public class GetRenderingsFromContextProcessor : IHeadlessGetRenderingsProcessor
    {
        /// <summary>
        ///     Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Process(GetRenderingsArgs args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));

            if (args.Result != null)
                return;
            args.Result = args.PageContext.PageDefinition.Renderings;
        }
    }
}