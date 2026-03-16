using System;
using System.Collections.Generic;

namespace CleanArchitecture.Core.Entities
{
    public class Comment : BaseEntity<Guid>
    {
        public Guid TrackId { get; set; }
        public Track Track { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid? ParentId { get; set; }
        public Comment Parent { get; set; }
        
        public ICollection<Comment> Replies { get; set; }
    }
}
