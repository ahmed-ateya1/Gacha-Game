using Gacha_Game.Core.Helper;

namespace Gacha_Game.Core.Dtos.AuthenticationDto
{
    public class RegisterDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RolesOption Role { get; set; } = RolesOption.USER;
    }
}
