using HeadlessSc.Pipelines.GetModel;

namespace HeadlessSc.Pipelines.GetFieldModel
{
    /// <summary>
    ///     Interface for custom processors in the headless.getModel pipeline.
    /// </summary>
    interface IHeadlessGetFieldModelPipeline
    {
        /// <summary>
        ///     Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        void Process(GetFieldModelArgs args);
    }
}
