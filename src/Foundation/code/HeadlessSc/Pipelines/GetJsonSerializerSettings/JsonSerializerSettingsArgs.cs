using Newtonsoft.Json;
using Sitecore.Mvc.Pipelines;

namespace HeadlessSc.Pipelines.GetJsonSerializerSettings
{
    public class JsonSerializerSettingsArgs : MvcPipelineArgs
    {
        public JsonSerializerSettings Result { get; set; }
    }
}