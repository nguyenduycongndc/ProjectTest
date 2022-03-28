using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebReport.ExecuteModels
{
    public class RepostModel
    {
        [JsonPropertyName("id")]
        public int id { get; set; }
        [JsonPropertyName("name")]
        public string name { get; set; }
        [JsonPropertyName("department")]
        public string department { get; set; }
        [JsonPropertyName("branch")]
        public string branch { get; set; }
    }
    public class RepostSearchModel
    {
        [JsonPropertyName("name")]
        public string name { get; set; }
        [JsonPropertyName("department")]
        public string department { get; set; }
        [JsonPropertyName("branch")]
        public string branch { get; set; }
    }
}
