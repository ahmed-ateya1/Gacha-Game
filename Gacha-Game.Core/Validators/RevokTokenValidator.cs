using FluentValidation;
using Gacha_Game.Core.Dtos.AuthenticationDto;

namespace Gacha_Game.Core.Validators
{
    public class RevokTokenValidator : AbstractValidator<RevokTokenDTO>
    {
        public RevokTokenValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token is required");
        }
    }
}
