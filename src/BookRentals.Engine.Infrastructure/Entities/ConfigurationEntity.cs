using BookRentals.Core.Infrastructure.Entities;

namespace BookRentals.Engine.Infrastructure.Entities
{
    public class ConfigurationEntity : AuditableEntity
    {
        public string Reference { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string ConfigurationKey { get; set; }
    }
}
