using System;

namespace CleanArchitecture.Core.DTOs.Account
{
    public class UserProfileResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string BackgroundPhotoUrl { get; set; }
        public System.Collections.Generic.IList<string> Roles { get; set; }
        public CleanArchitecture.Core.Enums.UserRole Role { get; set; }
    }
}
