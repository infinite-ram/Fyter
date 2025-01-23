using Fyter.CoreBusiness.FighterRequestModel;
using Fyter.CoreBusiness.FighterRequestModel.ViewModel;
using Fyter.UseCases.PluginInterfaces;

namespace Fyter.Plugins.Filters;

public class FighterStatusFilterRepository : IFighterStatusFilterRepository
{
    private IQueryable<FighterRequestViewModel> _query;
    private FighterUpdateStatusEnum? _selectedStatus;
    private FighterRequestViewEnum? _selectedView;

    private string? _currentUserId;

    private readonly IFighterRepository _fighterRepository;
    private readonly IFighterRequestRepository _fighterRequestRepository;

    public FighterStatusFilterRepository(
        IFighterRepository fighterRepository,
        IFighterRequestRepository fighterRequestRepository
    )
    {
        _fighterRepository = fighterRepository;
        _fighterRequestRepository = fighterRequestRepository;
    }

    public Task SetFightersAsQueryable(IQueryable<FighterRequestViewModel> query)
    {
        _query = query;
        return Task.CompletedTask;
    }

    public Task<IQueryable<FighterRequestViewModel>> GetFightersAsQueryable()
    {
        return Task.FromResult(_query);
    }

    public async Task SetFighterStatusFilter(FighterUpdateStatusEnum? status)
    {
        _selectedStatus = status;
        await ApplyFilters();
    }

    public async Task SetFighterRequestViewFilter(FighterRequestViewEnum? view)
    {
        _selectedView = view;
        await ApplyFilters();
    }

    private async Task ApplyFilters()
    {
        await ResetQuery();

        if (_selectedStatus.HasValue)
            _query = _query.Where(f => f.UpdateStatusType == _selectedStatus.Value);

        if (_selectedView.HasValue)
        {
            if (
                _selectedView.Value == FighterRequestViewEnum.Mine
                && !string.IsNullOrEmpty(_currentUserId)
            )
            {
                _query = _query.Where(f => f.UserId == _currentUserId);
            }
            else if (_selectedView.Value == FighterRequestViewEnum.All) { }
        }
    }

    public async Task SetCurrentUser(string userId)
    {
        _currentUserId = userId;
    }

    public async Task<List<FighterRequestViewModel>> GetCurrentUserFighterRequestList(string userId)
    {
        var fighterRequests = await CreateFightersStatusList();
        return fighterRequests.Where(c => c.UserId == userId).ToList();
    }

    public async Task<List<FighterRequestViewModel>> CreateFightersStatusList()
    {
        var fighterOutdatedRequests = await _fighterRepository.GetOutdatedFightersAsync("");

        var fighterViewModel = new List<FighterRequestViewModel>();
        fighterViewModel.AddRange(
            fighterOutdatedRequests.Select(f => new FighterRequestViewModel
            {
                Name = (f.FirstName + " " + f.LastName),
                UpdateStatusType = FighterUpdateStatusEnum.NeedToBeUpdated,
                OutedatedFighterId = f.FighterId,
                IsOutdated = f.IsOutdated,
                UserId = f.OutdatedByUserId ?? string.Empty,
                DateCreated = f.OutdatedRequestCreatedAt,
            })
        );

        var fighterAddRequests = await _fighterRequestRepository.GetFighterRequestsByNameAsync("");
        fighterViewModel.AddRange(
            fighterAddRequests.Select(f => new FighterRequestViewModel
            {
                Name = f.FullName,
                UpdateStatusType = FighterUpdateStatusEnum.NeedToBeAdded,
                FighterRequestId = f.Id,
                FighterRequestHasBeenAdded = f.HasBeenAdded,
                UserId = f.AddByUserId,
                DateCreated = f.DateCreated,
            })
        );

        return fighterViewModel
            .Where(f => f.IsOutdated == true && f.FighterRequestHasBeenAdded == false)
            .ToList();
    }

    public async Task ResetQuery()
    {
        var fighterUpdateRequests = await _fighterRepository.GetOutdatedFightersAsync("");
        var fighterAddRequests = await _fighterRequestRepository.GetFighterRequestsByNameAsync("");

        var fightersView = await CreateFightersStatusList();

        _query = fightersView.AsQueryable();
    }
}
