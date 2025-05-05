using Gacha_Game.Core.Dtos.CharacterDto;
using Gacha_Game.Core.Dtos.SpinDto;

namespace Gacha_Game.Core.ServiceContract
{
    public interface ISpinService
    {
        Task<GachaSpinResponse> SpinAsync(SpinAddRequest request);
    }
}
