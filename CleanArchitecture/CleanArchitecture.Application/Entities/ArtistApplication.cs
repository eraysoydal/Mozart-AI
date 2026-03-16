using System;
using CleanArchitecture.Core.Enums;

namespace CleanArchitecture.Core.Entities
{
    public class ArtistApplication : BaseEntity<Guid>
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string IdProofUrl { get; set; }
        public string PortfolioLinks { get; set; }
        
        public ApplicationStatus StatusId { get; set; }
        
        public string ReviewedBy { get; set; }
        public User Reviewer { get; set; }
    }
}
