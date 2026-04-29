using System;

namespace CleanArchitecture.Core.Entities
{
    public abstract class AuditableBaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }

        // Soft Delete
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
        public string DeletedBy { get; set; }
    }

    public abstract class AuditableBaseEntity<TId> : AuditableBaseEntity
    {
        public virtual TId Id { get; set; }
    }
}
