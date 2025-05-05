using FluentValidation;
using Gacha_Game.Core.Dtos.CharacterDto;

namespace Gacha_Game.Core.Validators
{
    public class characterUpdateRequestValidator : AbstractValidator<CharacterUpdateRequest>
    {
        public characterUpdateRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Name is required.")
                .MaximumLength(50)
                .WithMessage("Name must not exceed 50 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description must not exceed 500 characters.");
            RuleFor(x => x.Image)
                .NotNull()
                .WithMessage("Image is required.")
                .Must(x => x.Length > 0)
                .WithMessage("Image must not be empty.");
            RuleFor(x => x.AttackBase)
                .GreaterThan(0)
                .WithMessage("AttackBase must be greater than 0.");
            RuleFor(x => x.DefenseBase)
                .GreaterThan(0)
                .WithMessage("DefenseBase must be greater than 0.");
            RuleFor(x => x.HealthBase)
                .GreaterThan(0)
                .WithMessage("HealthBase must be greater than 0.");
            RuleFor(x => x.RarityId)
                .NotEmpty()
                .WithMessage("RarityId is required.");
            RuleFor(x => x.ElementTypeId)
                .NotNull()
                .WithMessage("ElementTypeId is required.");

            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("Id is required.");
        }
    }
}
