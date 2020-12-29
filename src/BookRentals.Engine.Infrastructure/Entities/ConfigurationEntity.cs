using BookRentals.Core.Infrastructure.Entities;
using System;

namespace BookRentals.Engine.Infrastructure.Entities
{
    public class ConfigurationEntity : AuditableEntity
    {
        public int ConfigurationId { get; set; }
        public string Reference { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string ConfigurationKey { get; set; }
    }
}
