namespace HeadlessSc.Pipelines.GetRenderings
{
    /// <summary>
    ///     Interface for custom processors in the headless.getRenderings pipeline.
    /// </summary>
    public interface IHeadlessGetRenderingsProcessor
    {
        /// <summary>
        ///     Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        void Process(GetRenderingsArgs args);
    }
}