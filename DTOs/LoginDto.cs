﻿namespace Iu_InstaShare_Api.DTOs
{
    public class LoginDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
