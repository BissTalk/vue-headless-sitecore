namespace HeadlessSc.Pipelines.GetJsonSerializerSettings
{
    /// <summary>
    ///     Interface for custom processors in the headless.getJsonSerializerSettings pipeline.
    /// </summary>
    interface IGetJsonSerializerSettingsPipeline
    {
        /// <summary>
        ///     Processes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        void Process(JsonSerializerSettingsArgs args);
    }
}
