using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.JSInterop;
using Fyter.CoreBusiness.FighterModel;
using Fyter.WebApp.DTOs;

namespace Fyter.WebApp.Utilities;

public static class DeveloperUtilities
{
    private static readonly Random random = new Random();

    public static bool IsPreRendering(IJSRuntime runtime)
    {
        // The peculiar thing in prerender is that Blazor circuit isn't yet created, so we can't use JSInterop
        return !(bool)runtime.GetType().GetProperty("IsInitialized").GetValue(runtime);
    }

    public static void FillFormWithRandomValues(FighterDto fighter)
    {
        // Skip FirstName and LastName
        fighter.FighterStars = GetRandomDouble(1.0, 5.0, 0.5);
        fighter.FighterStandUpStars = GetRandomDouble(1.0, 5.0, 0.5);
        fighter.FighterGrapplingStars = GetRandomDouble(1.0, 5.0, 0.5);
        fighter.FighterHealthStars = GetRandomDouble(1.0, 5.0, 0.5);
        fighter.EgoName = $"Ego-{random.Next(100, 999)}";
        fighter.FighterStyle = GetRandomEnum<FighterStyleEnum>();
        fighter.Division = GetRandomEnum<DivisionEnum>();

        // Fill StandUp stats
        foreach (var prop in typeof(StandUpDto).GetProperties())
        {
            var stat = (StatDto)prop.GetValue(fighter.StandUp);
            stat.Value = (int)GetRandomDouble(1.0, 5.0, 0.5);
            stat.Stars = GetRandomDouble(1.0, 5.0, 0.5);
        }

        // Fill Grappling stats
        foreach (var prop in typeof(GrapplingDto).GetProperties())
        {
            var stat = (StatDto)prop.GetValue(fighter.Grappling);
            stat.Value = (int)GetRandomDouble(1.0, 5.0, 0.5);
            stat.Stars = GetRandomDouble(1.0, 5.0, 0.5);
        }

        // Fill Health stats
        foreach (var prop in typeof(HealthDto).GetProperties())
        {
            var stat = (StatDto)prop.GetValue(fighter.Health);
            stat.Value = (int)GetRandomDouble(1.0, 5.0, 0.5);
            stat.Stars = GetRandomDouble(1.0, 5.0, 0.5);
        }

        // Fill FighterInfo
        fighter.FighterInfo.NickName = $"Nickname-{random.Next(100, 999)}";
        fighter.FighterInfo.Age = random.Next(18, 45);
        fighter.FighterInfo.Height.Feet = random.Next(4, 7);
        fighter.FighterInfo.Height.Inches = random.Next(0, 12);
        fighter.FighterInfo.Weight = random.Next(125, 265);
        fighter.FighterInfo.Reach = random.Next(60, 85);
        fighter.FighterInfo.Stance = GetRandomEnum<StanceEnum>();
        fighter.FighterInfo.HomeTown = $"Home-{random.Next(100, 999)}";
        fighter.FighterInfo.FightingOutOf = $"City-{random.Next(100, 999)}";

        // Fill Top Moves
        fighter.TopMoves.Clear();
        for (int i = 0; i < random.Next(1, 5); i++) // Add 1-5 random top moves
        {
            fighter.TopMoves.Add(
                new TopMovesDto
                {
                    MoveName = $"Move-{random.Next(1, 100)}",
                    Stars = GetRandomDouble(1.0, 5.0, 0.5),
                }
            );
        }
    }

    public static double GetRandomDouble(double min, double max, double step)
    {
        int steps = (int)((max - min) / step);
        return min + (random.Next(0, steps + 1) * step);
    }

    public static T GetRandomEnum<T>()
        where T : Enum
    {
        var values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(random.Next(values.Length));
    }

    public static void Log<T>(
        T value,
        bool linebreak = false,
        [CallerArgumentExpression("value")] string name = ""
    )
    {
        Console.ForegroundColor = ConsoleColor.Cyan;

        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(value, jsonOptions);

        if (value is string)
        {
            System.Console.WriteLine(json);
            if (linebreak)
                Console.WriteLine();
        }
        else
        {
            Console.WriteLine(json);
            Console.WriteLine();
        }

        Console.ResetColor();
    }

    public static void ClearLogs()
    {
        Console.Clear();
    }
}
