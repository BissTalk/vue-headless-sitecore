using System.Collections.Generic;
using Newtonsoft.Json;

namespace HeadlessSc.Areas.Headless.Models
{
    public class DataItem
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("fields")]
        public Dictionary<string, object> Fields { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}