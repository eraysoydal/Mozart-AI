namespace CleanArchitecture.Core.Entities
{
    public class Genre : AuditableBaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
