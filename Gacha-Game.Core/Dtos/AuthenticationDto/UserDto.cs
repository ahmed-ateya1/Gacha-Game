namespace Gacha_Game.Core.Dtos.AuthenticationDto
{
    public class UserDto
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Currancy { get; set; }
        public int Gems { get; set; }
        public int Level { get; set; }
        public int? Exp { get; set; }
    }
}
