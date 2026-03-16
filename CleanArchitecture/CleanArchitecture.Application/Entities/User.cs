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

        public UserRole RoleId { get; set; }
    }
}
