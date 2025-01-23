using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.PluginInterfaces;

namespace Fyter.Plugins.EFCoreSqlite;

public class FighterEFCoreRepository : IFighterRepository
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<FyterSqliteContext> contextFactory;

    public FighterEFCoreRepository(IDbContextFactory<FyterSqliteContext> contextFactory)
    {
        this.contextFactory = contextFactory;

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = config.CreateMapper();
    }

    public async Task AddFighterAsync(Fighter fighter)
    {
        fighter.LastUpdated = DateTime.UtcNow;

        using var db = this.contextFactory.CreateDbContext();
        db.Fighters?.Add(fighter);

        await db.SaveChangesAsync();
    }

    public async Task AddAlterEgoAsync(int primaryFighterId, Fighter alterEgo)
    {
        using var db = this.contextFactory.CreateDbContext();

        var baseFighter = await db
            .Fighters.Include(f => f.AlterEgos)
            .FirstOrDefaultAsync(f => f.FighterId == primaryFighterId && !f.IsAlterEgo);

        if (baseFighter == null)
            throw new Exception("Base Fighter not found or invalid for adding alter ego");

        alterEgo.IsAlterEgo = true;
        alterEgo.BaseFighterId = primaryFighterId;

        db.Fighters.Add(alterEgo);

        baseFighter.AlterEgos.Add(alterEgo);

        await db.SaveChangesAsync();
    }

    public async Task DeleteFighterAsync(Fighter fighter)
    {
        using var db = this.contextFactory.CreateDbContext();
        var fighterToBeDeleted = db
            .Fighters.Include(f => f.AlterEgos)
            .FirstOrDefault(f => f.FighterId == fighter.FighterId);

        if (fighterToBeDeleted != null)
            db.Fighters.Remove(fighterToBeDeleted);

        await db.SaveChangesAsync();
    }

    public async Task<List<Fighter>> GetAllBaseFightersAsync()
    {
        using var db = this.contextFactory.CreateDbContext();
        return await db.Fighters.Where(f => f.IsAlterEgo == false).ToListAsync();
    }

    public Task<Fighter> GetFighterByIdAsync(int fighterId)
    {
        using var db = this.contextFactory.CreateDbContext();
        var fighter = db
            .Fighters.Include(f => f.TopMoves)
            .Include(f => f.AlterEgos)
            .Include(f => f.BaseFighter)
            .FirstOrDefault(f => f.FighterId == fighterId);

        if (fighter != null)
            return Task.FromResult(fighter);

        return null;
    }

    public async Task<List<Fighter>> GetBaseFightersAsync(
        string searchQuery = "",
        int? limit = null
    )
    {
        using var db = this.contextFactory.CreateDbContext();

        var query = db.Fighters.Where(f => !f.IsAlterEgo);

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            query = query.Where(f =>
                (f.FirstName + " " + f.LastName).ToLower().IndexOf(searchQuery.ToLower()) >= 0
            );
        }

        if (limit.HasValue)
        {
            query = query.Take(limit.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<List<Fighter>> GetFightersByNameAsync(string fighterName)
    {
        using var db = this.contextFactory.CreateDbContext();
        if (string.IsNullOrWhiteSpace(fighterName))
            return await db.Fighters.ToListAsync();

        return await db
            .Fighters.Where(f =>
                (f.FirstName + " " + f.LastName).ToLower().IndexOf(fighterName.ToLower()) >= 0
            )
            .ToListAsync();
    }

    public async Task UpdateFighterAsync(Fighter fighter)
    {
        using var db = this.contextFactory.CreateDbContext();

        bool nameExists = await db.Fighters.AnyAsync(f =>
            f.FighterId != fighter.FighterId
            && (f.FirstName + " " + f.LastName).ToLower()
                == (fighter.FirstName + " " + fighter.LastName).ToLower()
        );

        if (nameExists)
            return;

        var fighterToUpdate = await db
            .Fighters.Include(f => f.TopMoves)
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.FighterId == fighter.FighterId);

        if (fighterToUpdate != null)
        {
            fighter.LastUpdated = DateTime.UtcNow;

            var topMovesToRemove = fighterToUpdate
                .TopMoves.Where(existingMove =>
                    !fighter.TopMoves.Any(updatedMove => updatedMove.Id == existingMove.Id)
                )
                .ToList();

            foreach (var move in topMovesToRemove)
            {
                fighterToUpdate.TopMoves.Remove(move);
            }

            foreach (var updatedMove in fighter.TopMoves)
            {
                var existingMove = fighterToUpdate.TopMoves.FirstOrDefault(m =>
                    m.Id == updatedMove.Id
                );
                if (existingMove != null)
                {
                    existingMove.MoveName = updatedMove.MoveName;
                    existingMove.Stars = updatedMove.Stars;
                }
                else
                {
                    fighterToUpdate.TopMoves.Add(
                        new TopMoves { MoveName = updatedMove.MoveName, Stars = updatedMove.Stars }
                    );
                }
            }

            fighterToUpdate.IsOutdated = fighter.IsOutdated;
            fighterToUpdate.OutdatedStats = fighter.OutdatedStats;

            _mapper.Map(fighter, fighterToUpdate);

            db.Update(fighterToUpdate);
            await db.SaveChangesAsync();
        }
    }

    public async Task SetOutdatedFighterStatsAsync(Fighter fighter, string userId)
    {
        using var db = this.contextFactory.CreateDbContext();

        Fighter fighterToUpdate = await db.Fighters.FirstOrDefaultAsync(f =>
            f.FighterId == fighter.FighterId
        );

        if (fighterToUpdate != null)
        {
            fighterToUpdate.IsOutdated = fighter.IsOutdated;
            fighterToUpdate.OutdatedStats = fighter.OutdatedStats;
            fighterToUpdate.OutdatedByUserId = userId;

            db.Update(fighterToUpdate);
            await db.SaveChangesAsync();
        }
    }

    public async Task SetUpdatedFighterStatsAsync(Fighter fighter)
    {
        using var db = this.contextFactory.CreateDbContext();

        Fighter fighterToUpdate = await db.Fighters.FirstOrDefaultAsync(f =>
            f.FighterId == fighter.FighterId
        );

        if (fighterToUpdate != null)
        {
            fighterToUpdate.IsOutdated = false;
            fighterToUpdate.OutdatedStats = new Dictionary<string, OutdatedStatus>();

            db.Update(fighterToUpdate);
            await db.SaveChangesAsync();
        }
    }

    public async Task<List<Fighter>> GetOutdatedFightersAsync(string fighterName)
    {
        using var db = this.contextFactory.CreateDbContext();

        var query = db.Fighters.Where(f => f.IsOutdated == true);

        if (!string.IsNullOrWhiteSpace(fighterName))
            query = query.Where(f =>
                (f.FirstName + " " + f.LastName).ToLower().Contains(fighterName.ToLower())
            );

        return await query.ToListAsync();
    }
}
