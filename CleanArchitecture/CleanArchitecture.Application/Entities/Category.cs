namespace CleanArchitecture.Core.Entities
{
    public class Category : AuditableBaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
