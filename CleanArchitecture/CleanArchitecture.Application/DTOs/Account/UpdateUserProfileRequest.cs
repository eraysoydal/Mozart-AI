using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Core.DTOs.Account
{
    public class UpdateUserProfileRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Biography { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string BackgroundPhotoUrl { get; set; }
    }
}
