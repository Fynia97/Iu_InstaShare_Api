using Iu_InstaShare_Api.DTO;
using Iu_InstaShare_Api.DTOs;
using Iu_InstaShare_Api.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Iu_InstaShare_Api.Models
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }


        public string Street { get; set; } = "";
        public string Zip { get; set; } = "";
        public string City { get; set; } = "";
        public string PhoneNumber { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = null;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
        public virtual ICollection<FriendsModel>? Friends { get; set; }

        public static UserTokenDto ToDto(UserProfileModel model, ITokenService tokenService)
        {
            return new UserTokenDto
            {
                Email = model.Email,
                Token = tokenService.CreateToken(model)
            };
        }
    }
}

