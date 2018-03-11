using System.Collections.Generic;
using Newtonsoft.Json;

namespace HeadlessSc.Areas.Headless.Models
{
    public class ComponentItem: ItemModel
    {
        [JsonProperty("componentName")]
        public string ComponentName { get; set; }

        [JsonProperty("parameters")]
        public Dictionary<string, string> Parameters { get; set; }

        [JsonIgnore]
        public int Sort{ get; set; }

        [JsonIgnore]
        public string PlaceholderName { get; set; }

        public override string ToString()
        {
            return $"{ComponentName} ({PlaceholderName})";
        }
    }
}