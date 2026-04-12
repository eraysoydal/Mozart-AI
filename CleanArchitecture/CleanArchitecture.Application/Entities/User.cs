using System;
using CleanArchitecture.Core.Enums;

namespace CleanArchitecture.Core.Entities
{
    public class User : BaseEntity<string>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string BackgroundPhotoUrl { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }

        public UserRole RoleId { get; set; }
    }
}
