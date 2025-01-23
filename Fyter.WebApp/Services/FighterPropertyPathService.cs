using System.Collections;
using System.Reflection;
using Fyter.CoreBusiness.FighterModel;
using Fyter.WebApp.Services.Interfaces;

namespace Fyter.WebApp.Services;

public class FighterPropertyPathService : IFighterPropertyPathService
{
    private static readonly HashSet<string> PropertiesToExclude =
        new()
        {
            "IsAlterEgo",
            "BaseFighterId",
            "BaseFighter",
            "AddedByUserId",
            "LastUpdatedByUserId",
            "IsOutdated",
            "FighterId",
        };
    private static readonly HashSet<string> CollectionsToInclude = new() { "Perks", "TopMoves" };

    public List<string> GetAllPropertyPaths(Fighter fighter)
    {
        return GetAllPropertyPathsInternal(fighter);
    }

    private List<string> GetAllPropertyPathsInternal(object obj, string parentPath = "")
    {
        var properties = new List<string>();
        var type = obj.GetType();

        foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var propName = prop.Name;
            if (PropertiesToExclude.Contains(propName))
                continue;

            var fullPath = string.IsNullOrEmpty(parentPath) ? propName : $"{parentPath}.{propName}";
            var propType = prop.PropertyType;

            if (IsSimpleType(propType))
            {
                properties.Add(fullPath);
            }
            else if (typeof(IEnumerable).IsAssignableFrom(propType) && propType != typeof(string))
            {
                if (CollectionsToInclude.Contains(propName))
                {
                    properties.Add(fullPath);
                }
            }
            else
            {
                var propValue = prop.GetValue(obj);
                if (propValue != null)
                {
                    properties.AddRange(GetAllPropertyPathsInternal(propValue, fullPath));
                }
                else
                {
                    properties.Add(fullPath);
                }
            }
        }

        return properties;
    }

    private bool IsSimpleType(Type type)
    {
        return type.IsPrimitive
            || type == typeof(string)
            || type.IsEnum
            || type == typeof(decimal)
            || type == typeof(DateTime)
            || (Nullable.GetUnderlyingType(type)?.IsEnum ?? false);
    }
}
