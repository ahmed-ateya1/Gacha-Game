﻿using System.ComponentModel.DataAnnotations;

namespace Gacha_Game.Core.Dtos.AuthenticationDto
{
    public class LoginDTO
    {
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
