using System.Collections.Generic;
using System.Web.Routing;
using Sitecore.Mvc.Pipelines.Request.RequestBegin;
using Sitecore.Mvc.Presentation;
using Sitecore.Pipelines;

namespace HeadlessSc.Pipelines.GetRenderings
{
    /// <summary>
    ///     <see cref="PipelineArgs" /> for custom used in the headless.getRenderings pipeline.
    /// </summary>
    /// <seealso cref="RequestBeginArgs" />
    public class GetRenderingsArgs : RequestBeginArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GetRenderingsArgs" /> class.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        public GetRenderingsArgs(RequestContext requestContext) : base(requestContext)
        {
        }

        /// <summary>
        ///     Gets or sets the result of the pipeline.
        /// </summary>
        /// <value>
        ///     The result.
        /// </value>
        public IEnumerable<Rendering> Result { get; set; }
    }
}