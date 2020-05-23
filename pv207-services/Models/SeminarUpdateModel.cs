using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace pv207_services.Models
{
    public class SeminarUpdateModel
    {
        public string Name { get; set; }

        public string Information { get; set; }

        public long? StartDate { get; set; }

        public int? Duration { get; set; }
    }
}
