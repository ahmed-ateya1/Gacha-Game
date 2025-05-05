using FluentValidation;
using Gacha_Game.Core.Dtos.GachaBannerDto;

namespace Gacha_Game.Core.Validators
{
    public class GachaBannerAddRequestValidator : AbstractValidator<GachaBannerAddRequest>
    {
        public GachaBannerAddRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotNull()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.StartDate)
                .NotNull()
                .WithMessage("Start date is required.")
                .Must(date => date >= DateTime.UtcNow)
                .WithMessage("Start date must be in the future.");

            RuleFor(x => x.EndDate)
                .NotNull()
                .WithMessage("End date is required.")
                .Must((request, endDate) => endDate > request.StartDate)
                .WithMessage("End date must be after the start date.");

            RuleFor(x => x.CostPerPull)
                .GreaterThan(0)
                .WithMessage("Cost per pull must be greater than 0.");

            RuleFor(x => x.PullsForGuaranteedRarity)
                .GreaterThan(0)
                .WithMessage("Pulls for guaranteed rarity must be greater than 0.");

            RuleFor(x => x.BannerImage)
                .NotNull()
                .WithMessage("Banner image is required.")
                .Must(file => file.Length > 0)
                .WithMessage("Banner image must not be empty.")
                .Must(file => file.ContentType is "image/jpeg" or "image/png")
                .WithMessage("Banner image must be a JPEG or PNG file.")
                .Must(file => file.Length <= 2 * 1024 * 1024)
                .WithMessage("Banner image must not exceed 2 MB.")
                .Must(file => file.FileName.Length <= 255)
                .WithMessage("Banner image file name must not exceed 255 characters.");
        }
    }
}