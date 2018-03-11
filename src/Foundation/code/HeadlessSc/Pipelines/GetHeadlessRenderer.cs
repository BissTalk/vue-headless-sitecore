using HeadlessSc.Layout;
using Sitecore.Mvc.Pipelines.Response.GetRenderer;

namespace HeadlessSc.Pipelines
{
    /// <summary>
    /// Renderer for Layout data.
    /// </summary>
    /// <seealso cref="Sitecore.Mvc.Pipelines.Response.GetRenderer.GetRendererProcessor" />
    public class GetHeadlessRenderer : GetRendererProcessor
    {
        /// <summary>
        /// Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public override void Process(GetRendererArgs args)
        {
            if (args.Result != null)
                return;
            if (!args.PageContext?.RequestContext?.HttpContext?.IsHeadless() ?? false)
                return;
            args.Result = new HeadlessRenderer(args.Rendering);
        }
    }
}