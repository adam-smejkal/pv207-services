using System;
using System.Collections.Generic;
using System.Text;

namespace pv207_services.Models
{
    public static class TableMappingExtensions
    {
        public static SeminarTableEntity ToTableEntity(this Seminar o) => new SeminarTableEntity()
        {
            ETag = "*",
            PartitionKey = "all",
            RowKey = o.Id,
            Id = o.Id,
            Name = o.Name,
            Information = o.Information,
            Duration = o.Duration,
            StartDate = o.StartDate,
        };

        public static Seminar ToModel(this SeminarTableEntity o) => new Seminar()
        {
            Id = o.Id,
            Name = o.Name,
            Information = o.Information,
            Duration = o.Duration,
            StartDate = o.StartDate
        };
    }
}
