namespace Gacha_Game.Core.Execptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string Message) : base(Message) { }
    }
}
