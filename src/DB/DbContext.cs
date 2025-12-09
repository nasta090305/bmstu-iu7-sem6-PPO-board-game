using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
// using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
// using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace DB;

public class PostgresDbContext : DbContext
{
    public DbSet<DbPlayer> Players { get; set; }
    public DbSet<DbDeckCard> Deck { get; set; }
    public DbSet<DbDiscard1Card> Discard1 { get; set; }
    public DbSet<DbDiscard2Card> Discard2 { get; set; }
    public DbSet<DbPlayersCard> PlayersCards { get; set; }
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbPlayer>().HasKey(p => p.Order);
        modelBuilder.Entity<DbDeckCard>().HasKey(c => c.Id);
        modelBuilder.Entity<DbDiscard1Card>().HasKey(c => c.Id);
        modelBuilder.Entity<DbDiscard2Card>().HasKey(c => c.Id);
        modelBuilder.Entity<DbPlayersCard>().HasKey(c => c.Id);
        modelBuilder.Entity<DbPlayersCard>().HasOne<DbPlayer>().WithMany().HasForeignKey(pc => pc.DbPlayerId);
    }
}
