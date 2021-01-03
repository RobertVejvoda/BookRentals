namespace BookRentals.Core.Infrastructure.Entities
{
    public class LanguageEntity : AuditableEntity
    {
        public string Caption { get; set; }
        public string CultureCode { get; set; }
    }
}
