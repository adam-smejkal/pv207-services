using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace pv207_services.Models
{
    public class SeminarTableEntity : TableEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public long StartDate { get; set; }
        public int Duration { get; set; }

        public SeminarTableEntity()
        {
        }
    }
}
