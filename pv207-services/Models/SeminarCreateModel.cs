using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace pv207_services.Models
{
    public class SeminarCreateModel
    {
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Information { get; set; }

        [JsonProperty(Required = Required.Always)]
        public long StartDate { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Duration { get; set; }
    }
}