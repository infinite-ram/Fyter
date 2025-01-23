using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Fyter.CoreBusiness.FighterModel;
using Fyter.CoreBusiness.FighterRequestModel;

namespace Fyter.Plugins.EFCoreSqlite;

public class FyterSqliteContext : DbContext
{
    public DbSet<Fighter> Fighters { get; set; }
    public DbSet<FighterRequest> FighterRequests { get; set; }

    public FyterSqliteContext(DbContextOptions<FyterSqliteContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fighter>(entity =>
        {
            entity.HasKey(f => f.FighterId);

            entity.Property(f => f.FirstName).IsRequired();
            entity.Property(f => f.LastName).IsRequired();
            entity.Property(f => f.EgoName).IsRequired();
            entity.Property(f => f.FighterStyle).HasConversion<string>();

            entity.Property(f => f.OutdatedByUserId).HasMaxLength(450).IsRequired(false);
            entity.Property(f => f.OutdatedRequestCreatedAt);

            entity
                .HasMany(f => f.AlterEgos)
                .WithOne(f => f.BaseFighter)
                .HasForeignKey(f => f.BaseFighterId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure StandUp as an owned type
            entity.OwnsOne(
                f => f.StandUp,
                sa =>
                {
                    sa.OwnsOne(s => s.PunchSpeed);
                    sa.OwnsOne(s => s.PunchPower);
                    sa.OwnsOne(s => s.Accuracy);
                    sa.OwnsOne(s => s.Blocking);
                    sa.OwnsOne(s => s.FootWork);
                    sa.OwnsOne(s => s.KickPower);
                    sa.OwnsOne(s => s.KickSpeed);
                    sa.OwnsOne(s => s.HeadMovement);
                    sa.OwnsOne(s => s.SwitchStance);
                    sa.OwnsOne(s => s.TakedownDefense);
                }
            );

            // Configure Grappling as an owned type
            entity.OwnsOne(
                f => f.Grappling,
                ga =>
                {
                    ga.OwnsOne(g => g.TakeDowns);
                    ga.OwnsOne(g => g.TopControl);
                    ga.OwnsOne(g => g.BottomControl);
                    ga.OwnsOne(g => g.ClinchControl);
                    ga.OwnsOne(g => g.ClinchStriking);
                    ga.OwnsOne(g => g.GroundStriking);
                    ga.OwnsOne(g => g.SubmissionDefense);
                    ga.OwnsOne(g => g.SubmissionOffense);
                }
            );

            // Configure Health as an owned type
            entity.OwnsOne(
                f => f.Health,
                ha =>
                {
                    ha.OwnsOne(h => h.Body);
                    ha.OwnsOne(h => h.Chin);
                    ha.OwnsOne(h => h.Legs);
                    ha.OwnsOne(h => h.Cardio);
                    ha.OwnsOne(h => h.Recovery);
                    ha.OwnsOne(h => h.CutResistant);
                }
            );

            // Configure FighterInfo as an owned type
            entity.OwnsOne(
                f => f.FighterInfo,
                fi =>
                {
                    fi.Property(f => f.Stance).HasConversion<string>().IsRequired(); // Convert enum to string
                    fi.OwnsOne(
                        f => f.Height,
                        fh =>
                        {
                            fh.Property(h => h.Feet).IsRequired();
                            fh.Property(h => h.Inches).IsRequired();
                        }
                    );
                }
            );

            entity
                .HasMany(f => f.TopMoves)
                .WithOne()
                .HasForeignKey("FighterId")
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasMany(f => f.AlterEgos)
                .WithOne()
                .HasForeignKey("BaseFighterId")
                .OnDelete(DeleteBehavior.Cascade);

            var perksComparer = new ValueComparer<List<PerksEnum>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()
            );

            entity
                .Property(f => f.Perks)
                .HasConversion(
                    v => string.Join(",", v),
                    v =>
                        v.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(e => Enum.Parse<PerksEnum>(e))
                            .ToList()
                )
                .Metadata.SetValueComparer(perksComparer);

            entity.Property(f => f.Division).HasConversion<string>().IsRequired();

            entity.Property(f => f.IsOutdated).HasDefaultValue(false);

            entity
                .Property(f => f.OutdatedStats)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v =>
                        JsonSerializer.Deserialize<Dictionary<string, OutdatedStatus>>(
                            v,
                            (JsonSerializerOptions)null
                        ) ?? new Dictionary<string, OutdatedStatus>()
                )
                .Metadata.SetValueComparer(
                    new ValueComparer<Dictionary<string, OutdatedStatus>>(
                        (d1, d2) => d1.SequenceEqual(d2),
                        d =>
                            d.Aggregate(
                                0,
                                (a, kvp) =>
                                    HashCode.Combine(
                                        a,
                                        kvp.Key.GetHashCode(),
                                        kvp.Value.IsOutdated.GetHashCode()
                                    )
                            ),
                        d =>
                            d.ToDictionary(
                                kvp => kvp.Key,
                                kvp => new OutdatedStatus { IsOutdated = kvp.Value.IsOutdated }
                            )
                    )
                );
        });

        modelBuilder.Entity<TopMoves>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.MoveName).IsRequired();
            entity.Property(t => t.Stars).IsRequired();
        });

        modelBuilder.Entity<FighterRequest>(entity =>
        {
            entity.HasKey(f => f.Id);
            entity.Property(f => f.FirstName).IsRequired();
            entity.Property(f => f.LastName).IsRequired();
            entity.Property(f => f.HasBeenAdded).IsRequired();
            entity.Property(f => f.AddByUserId).IsRequired();
        });
    }
}
