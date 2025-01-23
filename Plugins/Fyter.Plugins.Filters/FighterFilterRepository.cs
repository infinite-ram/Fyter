using AutoMapper;
using Fyter.CoreBusiness.FighterModel;
using Fyter.CoreBusiness.Filters;
using Fyter.CoreBusiness.Filters.Attributes;
using Fyter.Plugins.Filters.Extensions;
using Fyter.UseCases.PluginInterfaces;

namespace Fyter.Plugins.Filters;

public class FighterFilterRepository : IFighterFilterRepository
{
    public event Action? OnFilterChanged;

    public IQueryable<Fighter> _query;

    private readonly IFighterRepository fighterRepository;
    private readonly IMapper _mapper;

    private FighterStyleEnum? _selectedFighterStyle;
    public FighterStyleEnum? SelectedFighterStyle => _selectedFighterStyle;

    private DivisionEnum? _selectedDivision;
    public DivisionEnum? SelectedDivision => _selectedDivision;

    private PerksEnum? _selectedPerk;
    public PerksEnum? SelectedPerk => _selectedPerk;

    private FighterStandUpFilter _standUpFilter = new FighterStandUpFilter();
    public FighterStandUpFilter StandUpFilter => _standUpFilter;

    private FighterGrapplingFilter _grapplingFilter = new FighterGrapplingFilter();
    public FighterGrapplingFilter GrapplingUpFilter => _grapplingFilter;

    private FighterHealthFilter _healthFilter = new FighterHealthFilter();
    public FighterHealthFilter HealthFilter => _healthFilter;

    public Dictionary<string, string> ActiveFilterDisplays { get; set; } =
        new(StringComparer.OrdinalIgnoreCase);

    public FighterFilterRepository(IFighterRepository fighterRepository)
    {
        this.fighterRepository = fighterRepository;

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = config.CreateMapper();
    }

    public Task SetFightersAsQueryable(IQueryable<Fighter> query)
    {
        _query = query;
        return Task.CompletedTask;
    }

    public Task<IQueryable<Fighter>> GetFightersAsQueryable()
    {
        return Task.FromResult(_query);
    }

    public async Task SetFighterStyleFilter(FighterStyleEnum? fighterStyle)
    {
        _selectedFighterStyle = fighterStyle;

        if (fighterStyle.HasValue)
        {
            string styleName = fighterStyle.Value.ToString();
            ActiveFilterDisplays["FighterStyle"] = $"Fighter Style: {styleName}";
        }
        else
        {
            ActiveFilterDisplays.Remove("FighterStyle");
        }

        await ApplyFilters();

        NotifyFilterChanged();
    }

    public async Task SetFighterDivisionFilter(DivisionEnum? division)
    {
        _selectedDivision = division;
        if (division.HasValue)
            ActiveFilterDisplays["Division"] = $"Division: {division.Value}";
        else
            ActiveFilterDisplays.Remove("Division");
        await ApplyFilters();

        NotifyFilterChanged();
    }

    public async Task SetFighterPerkFilter(PerksEnum? perk)
    {
        _selectedPerk = perk;
        if (perk.HasValue)
            ActiveFilterDisplays["Perk"] = $"Perk: {perk.Value}";
        else
            ActiveFilterDisplays.Remove("Perk");
        await ApplyFilters();

        NotifyFilterChanged();
    }

    public async Task SetStandUpFilter(FighterStandUpFilter? standUpFilters)
    {
        _mapper.Map(standUpFilters, _standUpFilter);

        UpdateStandUpFilterDisplays(_standUpFilter);

        await ApplyFilters();
        NotifyFilterChanged();
    }

    public async Task SetGrapplingFilter(FighterGrapplingFilter? grapplingFilters)
    {
        _mapper.Map(grapplingFilters, _grapplingFilter);

        UpdateGrapplingFilterDisplays(_grapplingFilter);

        await ApplyFilters();
        NotifyFilterChanged();
    }

    public async Task SetHealthFilter(FighterHealthFilter? healthFilters)
    {
        _mapper.Map(healthFilters, _healthFilter);

        UpdateHealthFilterDisplays(_healthFilter);

        await ApplyFilters();
        NotifyFilterChanged();
    }

    public async Task ResetQuery()
    {
        _query = (await fighterRepository.GetFightersByNameAsync("")).AsQueryable();
    }

    private async Task ApplyFilters()
    {
        await ResetQuery();

        _query = _query.ApplyBasicFilters(_selectedFighterStyle, _selectedDivision, _selectedPerk);
        _query = _query.ApplyStandUpFilters(_standUpFilter);
        _query = _query.ApplyGrapplingFilters(_grapplingFilter);
        _query = _query.ApplyHealthFilters(_healthFilter);

        _query = _query.Where(f => f.IsAlterEgo == false);
    }

    public bool IsStandUpFilterApplied()
    {
        return _standUpFilter.HasFilters();
    }

    public bool IsGrapplingFilterApplied()
    {
        return _grapplingFilter.HasFilters();
    }

    public async Task ResetStandUpFilters()
    {
        _standUpFilter = new FighterStandUpFilter();
        await ApplyFilters();
    }

    public async Task ResetGrapplingFilters()
    {
        _grapplingFilter = new FighterGrapplingFilter();
        await ApplyFilters();
    }

    public bool IsHealthFilterApplied()
    {
        return _healthFilter.HasValue();
    }

    public async Task ResetHealthFilters()
    {
        _healthFilter = new FighterHealthFilter();
        await ApplyFilters();
    }

    private void UpdateStandUpFilterDisplays(FighterStandUpFilter filter)
    {
        // 1) Remove any old "Min*" / "Max*" entries from the dictionary,
        //    so we start fresh each time.
        var standUpKeys = ActiveFilterDisplays
            .Keys.Where(k => k.StartsWith("Min") || k.StartsWith("Max"))
            .ToList();
        foreach (var key in standUpKeys)
        {
            ActiveFilterDisplays.Remove(key);
        }

        // 2) Reflect over all properties in FighterStandUpFilter
        var props = typeof(FighterStandUpFilter).GetProperties();
        foreach (var prop in props)
        {
            // 3) We only care if the property actually has a value (i.e., it's not null).
            //    If it's an int?, cast it and check if it's non-null.
            var rawValue = prop.GetValue(filter);
            if (rawValue is int intValue)
            {
                // 4) Check if the property has the [StandUpFilterDisplay(...)] attribute.
                var attr =
                    prop.GetCustomAttributes(typeof(FilterDisplayAttribute), false).FirstOrDefault()
                    as FilterDisplayAttribute;
                if (attr != null)
                {
                    string displayText = $"{attr.DisplayName} {attr.OperatorString} {intValue}";
                    ActiveFilterDisplays[prop.Name] = displayText;
                }
            }
        }
    }

    private void UpdateGrapplingFilterDisplays(FighterGrapplingFilter filter)
    {
        // 1) Remove old grappling dictionary entries
        //    If you’re prefixing keys or have unique property names, you can do it differently.
        //    If property names might overlap, consider a prefix strategy (e.g. "Grappling_MinTakeDown").
        var grapplingKeys = ActiveFilterDisplays
            .Keys.Where(k => k.StartsWith("Min") || k.StartsWith("Max"))
            // If you want to be sure these belong to grappling, you might do more logic here
            .ToList();
        foreach (var key in grapplingKeys)
        {
            // If you share property names with other filters, consider a prefix approach or checks
            // For simplicity, we remove all "Min"/"Max" that belong to grappling
            // (We’ll address collisions in Step 4 if needed.)
            if (PropertyBelongsToClass(key, typeof(FighterGrapplingFilter)))
                ActiveFilterDisplays.Remove(key);
        }

        // 2) Reflect over all properties in FighterGrapplingFilter
        var props = typeof(FighterGrapplingFilter).GetProperties();
        foreach (var prop in props)
        {
            var rawValue = prop.GetValue(filter);
            if (rawValue is int intValue)
            {
                // 3) Check [FilterDisplay(...)] attribute
                var attr =
                    prop.GetCustomAttributes(typeof(FilterDisplayAttribute), false).FirstOrDefault()
                    as FilterDisplayAttribute;
                if (attr != null)
                {
                    // Build the display string, e.g. "Takedown ≥ 80"
                    string displayText = $"{attr.DisplayName} {attr.OperatorString} {intValue}";
                    // Use the property name as the key, e.g. "MinTakeDown"
                    ActiveFilterDisplays[prop.Name] = displayText;
                }
            }
        }
    }

    private void UpdateHealthFilterDisplays(FighterHealthFilter filter)
    {
        // 1) Remove old health dictionary entries
        var healthKeys = ActiveFilterDisplays
            .Keys.Where(k => k.StartsWith("Min") || k.StartsWith("Max"))
            .ToList();
        foreach (var key in healthKeys)
        {
            if (PropertyBelongsToClass(key, typeof(FighterHealthFilter)))
                ActiveFilterDisplays.Remove(key);
        }

        // 2) Reflect over all properties in FighterHealthFilter
        var props = typeof(FighterHealthFilter).GetProperties();
        foreach (var prop in props)
        {
            var rawValue = prop.GetValue(filter);
            if (rawValue is int intValue)
            {
                // 3) Check [FilterDisplay(...)] attribute
                var attr =
                    prop.GetCustomAttributes(typeof(FilterDisplayAttribute), false).FirstOrDefault()
                    as FilterDisplayAttribute;
                if (attr != null)
                {
                    string displayText = $"{attr.DisplayName} {attr.OperatorString} {intValue}";
                    ActiveFilterDisplays[prop.Name] = displayText;
                }
            }
        }
    }

    // Helper method to check if a given property name belongs to a certain filter class,
    // using reflection to see if that property name exists:
    private bool PropertyBelongsToClass(string propertyKey, Type filterType)
    {
        return filterType.GetProperty(propertyKey) != null;
    }

    private void NotifyFilterChanged()
    {
        OnFilterChanged?.Invoke();
    }
}
