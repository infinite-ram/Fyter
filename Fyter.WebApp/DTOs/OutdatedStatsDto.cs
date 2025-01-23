using Fyter.CoreBusiness.FighterModel;

namespace Fyter.WebApp.DTOs;

public class OutdatedStatsDto
{
    public Dictionary<string, OutdatedStatus> SelectedOutdatedStats { get; private set; } = new();

    public void Initialize(Fighter fighter, List<string> propertyPaths)
    {
        foreach (var prop in propertyPaths)
        {
            if (!SelectedOutdatedStats.ContainsKey(prop))
            {
                SelectedOutdatedStats[prop] = new OutdatedStatus();
            }

            if (fighter.OutdatedStats.TryGetValue(prop, out var status))
            {
                SelectedOutdatedStats[prop].IsOutdated = status.IsOutdated;
            }
            else
            {
                SelectedOutdatedStats[prop].IsOutdated = false;
            }
        }
    }

    public void SelectAll()
    {
        foreach (var status in SelectedOutdatedStats.Values)
        {
            status.IsOutdated = true;
        }
    }

    public void DeselectAll()
    {
        foreach (var status in SelectedOutdatedStats.Values)
        {
            status.IsOutdated = false;
        }
    }
}
