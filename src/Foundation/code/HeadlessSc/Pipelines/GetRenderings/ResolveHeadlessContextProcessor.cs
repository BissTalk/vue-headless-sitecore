using System;
using Sitecore.Pipelines;

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

            CorePipeline.Run("mvc.requestBegin", args);
        }
    }
}