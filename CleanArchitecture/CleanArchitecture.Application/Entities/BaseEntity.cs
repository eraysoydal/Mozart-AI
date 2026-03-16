namespace CleanArchitecture.Core.Entities
{
    public abstract class BaseEntity<TId>
    {
        public virtual TId Id { get; set; }
    }
}
