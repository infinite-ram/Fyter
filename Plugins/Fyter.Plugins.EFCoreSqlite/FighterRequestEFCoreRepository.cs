using Microsoft.EntityFrameworkCore;
using Fyter.CoreBusiness.FighterRequestModel;
using Fyter.UseCases.PluginInterfaces;

namespace Fyter.Plugins.EFCoreSqlite;

public class FighterRequestEFCoreRepository : IFighterRequestRepository
{
    private readonly IDbContextFactory<FyterSqliteContext> contextFactory;

    public FighterRequestEFCoreRepository(IDbContextFactory<FyterSqliteContext> contextFactory)
    {
        this.contextFactory = contextFactory;
    }

    public async Task AddFighterRequestAsync(FighterRequest fighter)
    {
        using var db = this.contextFactory.CreateDbContext();
        db.FighterRequests?.Add(fighter);

        await db.SaveChangesAsync();
    }

    public async Task DeleteFighterAsync(FighterRequest fighter)
    {
        using var db = this.contextFactory.CreateDbContext();
        var fighterToBeDeleted = db.FighterRequests.FirstOrDefault(f => f.Id == fighter.Id);

        if (fighterToBeDeleted != null)
            db.FighterRequests.Remove(fighterToBeDeleted);

        await db.SaveChangesAsync();
    }

    public Task<FighterRequest> GetFighterRequestByIdAsync(int fighterRequestId)
    {
        using var db = this.contextFactory.CreateDbContext();
        var fighter = db.FighterRequests.FirstOrDefault(f => f.Id == fighterRequestId);

        if (fighter != null)
            return Task.FromResult(fighter);

        return null;
    }

    public async Task<List<FighterRequest>> GetFighterRequestsByNameAsync(string fighterName)
    {
        using var db = this.contextFactory.CreateDbContext();
        if (string.IsNullOrWhiteSpace(fighterName))
            return await db.FighterRequests.ToListAsync();

        return await db
            .FighterRequests.Where(f =>
                (f.FirstName + " " + f.LastName).ToLower().IndexOf(fighterName.ToLower()) >= 0
            )
            .ToListAsync();
    }

    public async Task<bool> IsNameExistsAsync(string fullName, int excludeId = 0)
    {
        using var db = this.contextFactory.CreateDbContext();
        var fighterRequestNameExists = await db.FighterRequests.AnyAsync(f =>
            f.Id != excludeId && (f.FirstName + " " + f.LastName).ToLower() == fullName.ToLower()
        );
        var fighterNameExists = await db.Fighters.AnyAsync(f =>
            (f.FirstName.ToLower() + " " + f.LastName.ToLower()) == fullName.ToLower()
        );

        return fighterRequestNameExists || fighterNameExists;
    }

    public async Task UpdateFighterRequestAsync(FighterRequest fighter)
    {
        using var db = this.contextFactory.CreateDbContext();

        bool nameExists = await db.FighterRequests.AnyAsync(f =>
            f.Id != fighter.Id
            && (f.FirstName + " " + f.LastName).ToLower()
                == (fighter.FirstName + " " + fighter.LastName).ToLower()
        );

        if (nameExists)
            return;

        var fighterToUpdate = await db.FighterRequests.FirstOrDefaultAsync(f => f.Id == fighter.Id);

        if (fighterToUpdate != null)
        {
            fighterToUpdate.FirstName = fighter.FirstName;
            fighterToUpdate.LastName = fighter.LastName;
            fighterToUpdate.HasBeenAdded = fighter.HasBeenAdded;

            db.Update(fighterToUpdate);
            await db.SaveChangesAsync();
        }
    }
}
