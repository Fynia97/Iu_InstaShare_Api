using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Iu_InstaShare_Api.DTOs
{
    public class UserProfileDto
    {
        public string Email { get; set; } = "";
        public string Token { get; set; } = "";
    }
}
