using AutoMapper;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Fighters.Interfaces;
using Fyter.WebApp.DTOs;
using Fyter.WebApp.Services.Interfaces;

namespace Fyter.WebApp.Services;

public interface IAddFighterFormService
{
    bool IsAlterEgo { get; set; }

    event Action OnStateChanged;

    Task<Fighter?> SetBaseFighterAsync(int fighterId);
    Fighter? GetBaseFighter();
    FighterDto GetFighterViewModel();
    void SetFighterViewModel(FighterDto fighterDto);

    void Refresh();
    Fighter? ResetFighter();
}

public class AddFighterFormService : IAddFighterFormService
{
    public event Action OnStateChanged;
    private readonly IViewFighterByIdUseCase _viewFighterById;
    private readonly IFormDataCacheService _formDataCacheService;
    private readonly IMapper _mapper;

    private Fighter? baseFighter;
    private FighterDto _fighterDto = new FighterDto { EgoName = "Active" };
    private const string FormCacheKey = "AddFighterFormData";

    public bool IsAlterEgo
    {
        get => _fighterDto.IsAlterEgo;
        set
        {
            if (_fighterDto.IsAlterEgo != value)
            {
                _fighterDto.IsAlterEgo = value;
            }
        }
    }

    public AddFighterFormService(
        IViewFighterByIdUseCase viewFighterById,
        IFormDataCacheService formDataCacheService,
        IMapper mapper
    )
    {
        _viewFighterById = viewFighterById;
        _formDataCacheService = formDataCacheService;
        _mapper = mapper;
    }

    public async Task<Fighter?> SetBaseFighterAsync(int fighterId)
    {
        baseFighter = await _viewFighterById.ExecuteAsync(fighterId);

        if (baseFighter != null)
        {
            _mapper.Map(baseFighter, _fighterDto);
            ResetIds(_fighterDto);

            _fighterDto.IsAlterEgo = true;
            _fighterDto.BaseFighterId = baseFighter.FighterId;

            return baseFighter;
        }

        return null;
    }

    public Fighter? GetBaseFighter()
    {
        return (baseFighter != null) ? baseFighter : null;
    }

    public FighterDto GetFighterViewModel()
    {
        return _fighterDto;
    }

    public void SetFighterViewModel(FighterDto fighterDto)
    {
        this._fighterDto = fighterDto;
    }

    public async Task SaveFormDataToCache()
    {
        await _formDataCacheService.SaveFormDataAsync(FormCacheKey, _fighterDto);
    }

    public async Task LoadFormDataFromCache()
    {
        var cachedFighter = await _formDataCacheService.LoadFormDataAsync<FighterDto>(
            FormCacheKey
        );

        if (cachedFighter != null)
            _fighterDto = cachedFighter;
    }

    public void Refresh()
    {
        OnStateChanged.Invoke();
    }

    private void ResetIds(FighterDto fighter)
    {
        fighter.FighterId = 0;
        fighter.BaseFighter = null;
        fighter.AlterEgos = null;
    }

    public Fighter? ResetFighter()
    {
        baseFighter = null;

        _fighterDto = new();
        _fighterDto.EgoName = "Active";

        return null;
    }
}
