using FluentValidation;
using Gacha_Game.Core.Dtos.RarityDto;

namespace Gacha_Game.Core.Validators
{
    public class RarityAddRequestValidator : AbstractValidator<RarityAddRequest>
    {
        public RarityAddRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(50)
                .WithMessage("Name must not exceed 50 characters.");

            RuleFor(x => x.ColorCode)
                .Matches(@"^#([0-9A-Fa-f]{6}|[0-9A-Fa-f]{3})$")
                .WithMessage("Color code must be a valid hex color code.");

            RuleFor(x => x.DropRate)
                .NotEmpty()
                .WithMessage("Drop rate is required.")
                .InclusiveBetween(0, 100)
                .WithMessage("Drop rate must be between 0 and 100.");

        }

    }
}
