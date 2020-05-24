using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace pv207_services.Models
{
    public class LectureTableEntity : TableEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
    }
}
