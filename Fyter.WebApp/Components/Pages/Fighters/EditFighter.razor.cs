using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Fighters.Interfaces;
using Fyter.WebApp.Extensions;
using Fyter.WebApp.Services.Interfaces;

namespace Fyter.WebApp.Components.Pages.Fighters;

public partial class EditFighter : ComponentBase
{
    [Inject]
    public required IViewFighterByIdUseCase ViewFighterByIdUseCase { get; set; }

    [Inject]
    public required IUpdateFighterUseCase UpdateFighterUseCase { get; set; }

    [Inject]
    public required IDeleteFighterUseCase DeleteFighterUseCase { get; set; }

    [Inject]
    public required ISetFighterStatUpdatedUsecase SetFighterStatUpdatedUsecase { get; set; }

    [Inject]
    public required IAuthService AuthService { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Parameter]
    public int Id { get; set; }

    private Fighter fighter = new Fighter();

    private bool isAuthenticated = false;
    private bool isAuthorized = false;
    private bool isResolveFighter = false;

    private string searchTerm = string.Empty;
    private List<PerksEnum> FilteredPerks =>
        Enum.GetValues<PerksEnum>()
            .Where(perk =>
                perk.GetDisplayName().Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            )
            .ToList();

    private List<string>? outdatedProperties;
    private Fighter? originalFighter;
    private List<string>? needToBeChanged;
    private bool isStillOudated = false;

    protected override async Task OnInitializedAsync()
    {
        isAuthenticated = await AuthService.IsUserAuthenticatedAsync();
        isAuthorized = await AuthService.IsUserAuthorizedAsync(
            RoleConstants.Admin,
            RoleConstants.FighterEditor
        );
    }

    protected override async Task OnParametersSetAsync()
    {
        fighter = await ViewFighterByIdUseCase.ExecuteAsync(Id);

        var json = JsonSerializer.Serialize(fighter);
        originalFighter = JsonSerializer.Deserialize<Fighter>(json);

        outdatedProperties = fighter
            .OutdatedStats.Where(kv => kv.Value.IsOutdated)
            .Select(kv => kv.Key)
            .ToList();
    }

    private static object? GetNestedPropertyValue(object obj, string propertyPath)
    {
        foreach (var property in propertyPath.Split('.'))
        {
            if (obj == null)
                return null;

            var propInfo = obj.GetType().GetProperty(property);
            if (propInfo == null)
                return null;

            obj = propInfo.GetValue(obj);
        }
        return obj;
    }

    private async Task Update()
    {
        // 1) Gather outdated properties
        var outdatedProperties = fighter
            .OutdatedStats.Where(kv => kv.Value.IsOutdated)
            .Select(kv => kv.Key)
            .ToList();

        // 2) Check which outdated props were actually changed
        var changedOutdatedProps = new List<string>();
        foreach (var propertyPath in outdatedProperties)
        {
            var originalValue = GetNestedPropertyValue(originalFighter!, propertyPath);
            var currentValue = GetNestedPropertyValue(fighter, propertyPath);

            // Compare
            if (!Equals(originalValue, currentValue))
            {
                changedOutdatedProps.Add(propertyPath);
            }
        }

        needToBeChanged = new List<string>();
        foreach (var i in outdatedProperties)
        {
            needToBeChanged.Add(i);
        }

        foreach (var outdated in outdatedProperties)
        {
            foreach (var changedOutdated in changedOutdatedProps)
            {
                if (outdated.Equals(changedOutdated))
                {
                    needToBeChanged.Remove(outdated);
                }
            }
        }

        var userId = await AuthService.GetUserIdAsync();

        if (userId != null)
            fighter.LastUpdatedByUserId = userId;

        if (isResolveFighter)
        {
            if (needToBeChanged.Count() != 0)
            {
                isStillOudated = true;
                return;
            }
            fighter.IsOutdated = false;
            fighter.OutdatedStats.Clear();
        }

        await UpdateFighterUseCase.ExecuteAsync(fighter);
        NavigationManager.NavigateTo($"/fighter/{fighter.FighterId}", forceLoad: true);
    }

    private void OnPerkChanged(PerksEnum perk, bool isSelected)
    {
        if (isSelected)
        {
            if (!fighter.Perks.Contains(perk))
            {
                fighter.Perks.Add(perk);
            }
        }
        else
        {
            if (fighter.Perks.Contains(perk))
            {
                fighter.Perks.Remove(perk);
            }
        }
    }

    private void OnDivisionChanged(DivisionEnum division)
    {
        fighter.Division = division;
    }

    private void AddTopMove()
    {
        var newTopMove = new TopMoves
        {
            Id = 0,
            MoveName = string.Empty,
            Stars = 0.0,
        };
        fighter.TopMoves.Add(newTopMove);
    }

    private void RemoveTopMove(TopMoves topMove)
    {
        if (fighter.TopMoves.Contains(topMove))
        {
            fighter.TopMoves.Remove(topMove);
        }
    }

    private async Task Delete()
    {
        if (fighter != null)
            await DeleteFighterUseCase.ExecuteAsync(fighter);

        NavigationManager.NavigateTo("/");
    }

    private void TogglePerkSelection(PerksEnum perk)
    {
        if (fighter.Perks.Contains(perk))
        {
            fighter.Perks.Remove(perk);
        }
        else
        {
            fighter.Perks.Add(perk);
        }
    }

    private bool IsPropertyOutdated(string propertyName)
    {
        return fighter.OutdatedStats.TryGetValue(propertyName, out var status) && status.IsOutdated;
    }
}
