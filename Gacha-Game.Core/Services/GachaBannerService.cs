using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Domain.RepositoryContract;
using Gacha_Game.Core.Dtos.GachaBannerDto;
using Gacha_Game.Core.Execptions;
using Gacha_Game.Core.ServiceContract;
using Mapster;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Gacha_Game.Core.Services
{
    public class GachaBannerService(
        IUnitOfWork _unitOfWork,
        ILogger<GachaBannerService> _logger,
        IFileServices _fileService)
        : IGachaBannerService
    {

        private async Task ExecuteWithTransactionAsync(Func<Task> action)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    _logger.LogInformation("Transaction started.");
                    await action();
                    await _unitOfWork.CommitTransactionAsync();
                    _logger.LogInformation("Transaction committed successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Transaction failed. Rolling back...");
                    await _unitOfWork.RollbackTransactionAsync();
                    throw;
                }
            }
        }

        public async Task<GachaBannerResponse> AddGachaBannerAsync(GachaBannerAddRequest? request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            _logger.LogInformation("Adding new Gacha Banner...");
            var GachaBanner = request.Adapt<GachaBanners>();

            if (request.BannerImage != null)
            {
                _logger.LogInformation("Uploading Banner Image...");
                var fileName = await _fileService.CreateFile(request.BannerImage);
                GachaBanner.BannerImageUrl = fileName;
            }

            await ExecuteWithTransactionAsync(async () =>
            {
                await _unitOfWork.Repository<GachaBanners>()
                .CreateAsync(GachaBanner);
            });
            _logger.LogInformation("Gacha Banner added successfully.");
            return GachaBanner.Adapt<GachaBannerResponse>();
        }

        public async Task<bool> DeleteGachaBannerAsync(Guid id)
        {
            var gachaBanner = await _unitOfWork.Repository<GachaBanners>()
                .GetByAsync(x=>x.Id == id,includeProperties: "BannerCharacters");

            _logger.LogInformation($"Gacha Banner with ID : {id} Found");
            if (gachaBanner == null)
            {
                _logger.LogWarning($"Gacha Banner with ID : {id} Not Found");
                throw new NotFoundException($"Gacha Banner with ID : {id} Not Found");
            }
            await ExecuteWithTransactionAsync(async () =>
            {
                if (gachaBanner.BannerCharacters != null && gachaBanner.BannerCharacters.Any())
                {
                    foreach (var character in gachaBanner.BannerCharacters)
                    {
                        await _unitOfWork.Repository<BannerCharacter>()
                            .DeleteAsync(character);
                    }
                }
                await _unitOfWork.Repository<GachaBanners>()
                    .DeleteAsync(gachaBanner);
            });
            _logger.LogInformation($"Gacha Banner with ID : {id} Deleted Successfully");
            return true;
        }

        public async Task<IEnumerable<GachaBannerResponse>> GetAllGachaBanners(Expression<Func<GachaBanners, bool>>? expression = null)
        {
            var gachaBanners = await _unitOfWork.Repository<GachaBanners>()
                .GetAllAsync(expression);

            if (gachaBanners == null || !gachaBanners.Any())
            {
                _logger.LogWarning("No Gacha Banners found.");
                return Enumerable.Empty<GachaBannerResponse>();
            }
            return gachaBanners.Adapt<IEnumerable<GachaBannerResponse>>();
        }

        public async Task<GachaBannerResponse?> GetGachaBannerByAsync
            (Expression<Func<GachaBanners, bool>> expression)
        {
            var gachaBanner = await _unitOfWork.Repository<GachaBanners>()
                .GetByAsync(expression);
            if (gachaBanner == null)
            {
                _logger.LogWarning("Gacha Banner not found.");
                throw new NotFoundException("Gacha Banner not found.");
            }
            _logger.LogInformation("Gacha Banner found.");
            return gachaBanner.Adapt<GachaBannerResponse>();
        }

        public async Task<GachaBannerResponse> UpdateGachaBannerAsync
            (GachaBannerUpdateRequest? request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            _logger.LogInformation("Updating Gacha Banner...");
            var gachaBanner = await _unitOfWork.Repository<GachaBanners>()
                .GetByAsync(x => x.Id == request.Id);

            if (gachaBanner == null)
            {
                _logger.LogWarning($"Gacha Banner with ID : {request.Id} Not Found");
                throw new NotFoundException($"Gacha Banner with ID : {request.Id} Not Found");
            }
            request.Adapt(gachaBanner);
            if(request.BannerImage != null)
            {
                await _fileService.DeleteFile(gachaBanner.BannerImageUrl);
                _logger.LogInformation("Uploading Banner Image...");
                var fileName = await _fileService.CreateFile(request.BannerImage);
                gachaBanner.BannerImageUrl = fileName;
            }
            await ExecuteWithTransactionAsync(async () =>
            {
                await _unitOfWork.Repository<GachaBanners>()
                    .UpdateAsync(gachaBanner);
            });
            _logger.LogInformation("Gacha Banner updated successfully.");
            return gachaBanner.Adapt<GachaBannerResponse>();
        }
    }
}
