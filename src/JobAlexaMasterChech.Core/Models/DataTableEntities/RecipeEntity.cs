using Azure;
using Azure.Data.Tables;
using System;

namespace JobAlexaMasterChech.Core.Models.DataTableEntities
{
    public class RecipeEntity : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public string Title { get; set; }
    }
}
