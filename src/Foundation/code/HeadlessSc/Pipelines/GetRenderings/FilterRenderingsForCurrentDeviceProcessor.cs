using System;
using System.Linq;

namespace HeadlessSc.Pipelines.GetRenderings
{
    /// <summary>
    ///     Processor for filtering all the rendering to only those that apply to the current device.
    /// </summary>
    /// <seealso cref="IHeadlessGetRenderingsProcessor" />
    public class FilterRenderingsForCurrentDeviceProcessor: IHeadlessGetRenderingsProcessor
    {
        /// <summary>
        /// Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void Process(GetRenderingsArgs args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));

            args.Result = args.Result?.Where(r =>
                    r.DeviceId == args.PageContext.Device.Id
                    && !r.IsLayoutRendering()
                    && !string.IsNullOrEmpty(r.Placeholder)
                    && r.Renderer != null);
        }
    }
}