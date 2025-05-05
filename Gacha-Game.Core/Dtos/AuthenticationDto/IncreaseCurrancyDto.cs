namespace Gacha_Game.Core.Dtos.AuthenticationDto
{
    public class IncreaseCurrancyDto 
    {
        public Guid UserID { get; set; }
        public int Amount { get; set; }
    }
}
