using Fyter.UseCases.PluginInterfaces;
using Fyter.CoreBusiness.Exceptions;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Fighters.Interfaces;

namespace Fyter.UseCases.Fighters;

public class AddFighterUseCase : IAddFighterUseCase
{
    private readonly IFighterRepository fighterRepository;

    public AddFighterUseCase(IFighterRepository fighterRepository)
    {
        this.fighterRepository = fighterRepository;
    }

    public async Task ExecuteAsync(Fighter fighter)
    {
        if (!fighter.IsAlterEgo)
        {
            var existingFighter = await fighterRepository.GetFightersByNameAsync(
                fighter.GetFullName()
            );
            if (
                existingFighter.Any(f =>
                    !f.IsAlterEgo
                    && f.FirstName.Equals(fighter.FirstName, StringComparison.OrdinalIgnoreCase)
                    && f.LastName.Equals(fighter.LastName, StringComparison.OrdinalIgnoreCase)
                )
            )
            {
                throw new FighterNameAlreadyExistsException(
                    $"A fighter with the name '{fighter.GetFullName()}' already exists"
                );
            }
        }

        await fighterRepository.AddFighterAsync(fighter);
    }
}
