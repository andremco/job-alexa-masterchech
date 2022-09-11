using Azure;
using Azure.Data.Tables;
using System;

namespace JobAlexaMasterChech.Core.Models.DataTableEntities
{
    public class IngredientEntity : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public int ExternCode { get; set; }
        public string Description { get; set; }
    }
}
