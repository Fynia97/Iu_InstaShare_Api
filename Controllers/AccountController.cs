using Iu_InstaShare_Api.Configurations;
using Iu_InstaShare_Api.DTOs;
using Iu_InstaShare_Api.Interfaces;
using Iu_InstaShare_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Iu_InstaShare_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DataDbContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public ActionResult<UserProfileDto> Register(RegisterDto registerDto)
        {
            if (_context.UserProfiles.Any(x => x.Email.ToLower() == registerDto.Email.ToLower()))
            {
                return BadRequest();
            }

            using var hmac = new HMACSHA512();

            var user = new UserProfileModel
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.UserProfiles.Add(user);
            _context.SaveChanges();

            return UserProfileModel.ToDto(user, _tokenService);
        }

        [HttpPost("login")]
        public ActionResult<UserProfileDto> Login(LoginDto loginDto)
        {
            var user = _context.UserProfiles.FirstOrDefault(x => x.Email == loginDto.Email);

            if (user == null)
            {
                return BadRequest();
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return BadRequest();
                }

            }

            return UserProfileModel.ToDto(user, _tokenService);
        }
    }
}
