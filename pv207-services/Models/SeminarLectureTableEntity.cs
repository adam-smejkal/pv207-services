using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace pv207_services.Models
{
    public class SeminarLectureTableEntity : TableEntity
    {
        public string SeminarId { get; set; }
        public string LectureId { get; set; }
    }
}
