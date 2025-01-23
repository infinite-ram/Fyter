using Fyter.CoreBusiness.FighterRequestModel;
using Fyter.CoreBusiness.FighterRequestModel.ViewModel;

namespace Fyter.UseCases.PluginInterfaces;

public interface IFighterStatusFilterRepository
{
    Task SetFightersAsQueryable(IQueryable<FighterRequestViewModel> query);
    Task SetFighterStatusFilter(FighterUpdateStatusEnum? status);
    Task<IQueryable<FighterRequestViewModel>> GetFightersAsQueryable();
    Task<List<FighterRequestViewModel>> CreateFightersStatusList();

    Task<List<FighterRequestViewModel>> GetCurrentUserFighterRequestList(string userId);
    Task SetFighterRequestViewFilter(FighterRequestViewEnum? view);

    Task SetCurrentUser(string userId);

    Task ResetQuery();
}
