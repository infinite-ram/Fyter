using Fyter.CoreBusiness.FighterRequestModel;

namespace Fyter.UseCases.Filters.Interfaces
{
    public interface ISetFighterStatusFilterUseCase
    {
        Task ExecuteAsync(FighterUpdateStatusEnum? status);
    }
}
