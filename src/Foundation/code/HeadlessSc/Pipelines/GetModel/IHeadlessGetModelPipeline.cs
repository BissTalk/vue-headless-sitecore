namespace HeadlessSc.Pipelines.GetModel
{
    /// <summary>
    ///     Interface for custom processors in the headless.getModel pipeline.
    /// </summary>
    internal interface IHeadlessGetModelPipeline
    {
        /// <summary>
        ///     Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        void Process(GetModelArgs args);
    }
}