using FluentValidation;
using Gacha_Game.Core.Dtos.AuthenticationDto;

namespace Gacha_Game.Core.Validators
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailDTO>
    {
        public ConfirmEmailValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid");
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required");
        }
    }
}
