using FluentValidation;
using Gacha_Game.Core.CQRS.Queries.ElementTypeQueries;
using Gacha_Game.Core.MapsterConfiguration;
using Gacha_Game.Core.ServiceContract;
using Gacha_Game.Core.Services;
using Gacha_Game.Core.Validators;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gacha_Game.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services , IConfiguration configuration)
        {
            MapsterConfig.Configure();
            services.AddMediatR(
                cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllElementTypesQuery>()
            );
            services.AddValidatorsFromAssemblyContaining<ForgotPasswordValidator>();
            services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IFileServices, FileService>();
            services.AddScoped<IElementTypeService, ElementTypeService>();
            services.AddScoped<IRarityService, RarityService>();
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<IGachaBannerService, GachaBannerService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IBannerCharacterService, BannerCharacterService>();
            services.AddScoped<ISpinService, SpinService>();
            return services;
        }
    }
}
