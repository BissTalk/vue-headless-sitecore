using System.Collections.Generic;
using Newtonsoft.Json;

namespace HeadlessSc.Areas.Headless.Models
{
    public class ItemModel : DataItem
    {
        [JsonProperty("placeholders")] public Dictionary<string, List<ComponentItem>> Placeholders { get; set; }
    }
}