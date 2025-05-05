using Gacha_Game.Core.Domain.Entities;
using Gacha_Game.Core.Domain.RepositoryContract;
using Gacha_Game.Core.Dtos.ElementTypeDto;
using Gacha_Game.Core.Execptions;
using Gacha_Game.Core.ServiceContract;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Gacha_Game.Core.Services
{
    public class ElementTypeService(
        IUnitOfWork _unitOfWork, 
        ILogger<ElementTypeService> _logger,
        IFileServices _fileServices
        ) : IElementTypeService
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
        public async Task<ElementTypeResponse> CreateAsync(ElementTypeAddRequest? request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Request cannot be null");
            }
            var elementType = request.Adapt<ElementType>();
            elementType.Id = Guid.NewGuid();

            if (request.Icon != null)
            {
                elementType.IconUrl = await _fileServices.CreateFile(request.Icon);
            }

            await ExecuteWithTransactionAsync(async () =>
            {
                await _unitOfWork.Repository<ElementType>().CreateAsync(elementType);
                await _unitOfWork.CompleteAsync();
            });

            _logger.LogInformation("ElementType created successfully.");

            return elementType.Adapt<ElementTypeResponse>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var elementType = await _unitOfWork.Repository<ElementType>()
                .GetByAsync(x=>x.Id == id);

            if(elementType == null)
            {
                throw new ArgumentNullException(nameof(elementType), "ElementType not found");
            }
            await ExecuteWithTransactionAsync(async () =>
            {
                await _unitOfWork.Repository<ElementType>().DeleteAsync(elementType);
                await _unitOfWork.CompleteAsync();
            });

            _logger.LogInformation("ElementType deleted successfully.");

            if (elementType.IconUrl != null)
            {
                await _fileServices.DeleteFile(elementType.IconUrl);
            }
            return true;
        }

        public async Task<IEnumerable<ElementTypeResponse>> GetAllAsync(Expression<Func<ElementType, bool>>? predicate = null)
        {
            var elementTypes = await _unitOfWork.Repository<ElementType>()
               .GetAllAsync(predicate);

            if (!elementTypes.Any())
                return Enumerable.Empty<ElementTypeResponse>();

            return elementTypes.Adapt<IEnumerable<ElementTypeResponse>>();  
        }

        public async Task<ElementTypeResponse> GetByAsync(Expression<Func<ElementType, bool>> predicate, bool isTracked = false)
        {
            var elementType = await _unitOfWork.Repository<ElementType>()
                .GetByAsync(predicate, isTracked);

            if (elementType == null)
            {
                throw new NotFoundException("ElementType not found");
            }
            
            return elementType.Adapt<ElementTypeResponse>();

        }

        public async Task<ElementTypeResponse> UpdateAsync(ElementTypeUpdateRequest? request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var elementType = await _unitOfWork.Repository<ElementType>()
                .GetByAsync(x => x.Id == request.Id);

            if (elementType == null)
                throw new NotFoundException("Element Type Not Found");

            request.Adapt(elementType); 

            if (request.Icon != null)
            {
                if (elementType.IconUrl != null)
                {
                    await _fileServices.DeleteFile(elementType.IconUrl);
                }
                elementType.IconUrl = await _fileServices.CreateFile(request.Icon);
            }

            await ExecuteWithTransactionAsync(async () =>
            {
                await _unitOfWork.Repository<ElementType>().UpdateAsync(elementType);
                await _unitOfWork.CompleteAsync();
            });

            return elementType.Adapt<ElementTypeResponse>();
        }

    }
}
