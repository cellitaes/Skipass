namespace Skipass.Database;

using System;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Skipass.Domain;

public class SkipassDbContext : IdentityDbContext<IdentityUser>
{
    public const string SCHEMA = "skipass";

    public SkipassDbContext(DbContextOptions options) : base(options)
    { }

    public DbSet<Card> Cards { get; set; }
    public DbSet<Gate> Gates { get; set; }
    public DbSet<Passage> Passages { get; set; }
    public DbSet<PriceListItem> PriceList { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema(SCHEMA);

        builder.Entity<Card>(b =>
        {
            b.ToTable("Cards");
            b.HasKey(e => e.Identifier);

            b.HasData(new Card
            {
                Identifier = "921212",
                PassagesLeft = 2,
                ValidTo = DateTime.UtcNow,
            });
            b.HasData(new Card
            {
                Identifier = "792922",
                PassagesLeft = 15,
                ValidTo = DateTime.UtcNow,
            });
            b.HasData(new Card
            {
                Identifier = "533333",
                PassagesLeft = 0,
                ValidTo = DateTime.UtcNow + TimeSpan.FromMinutes(15),
            });
        });

        builder.Entity<Company>(b =>
        {
            b.ToTable("Companies");
            b.HasKey(e => e.Identifier);

            b.HasData(new
            {
                Identifier = 1,
                Name = "Mountain Resort Hotel",
            });
            b.HasData(new
            {
                Identifier = 2,
                Name = "Powder Lodge",
            });
            b.HasData(new
            {
                Identifier = 3,
                Name = "Ski Refuge",
            });
            b.HasData(new
            {
                Identifier = 4,
                Name = "The Elite Ski",
            });
        });

        builder.Entity<Gate>(b =>
        {
            b.ToTable("Gates");
            b.HasKey(e => e.Identifier);

            b.HasOne(e => e.Owner).WithMany();

            b.HasData(new
            {
                Identifier = "T1234",
                OwnerIdentifier = 1,
            });
            b.HasData(new
            {
                Identifier = "T9999",
                OwnerIdentifier = 2,
            });
        });

        builder.Entity<Passage>(b =>
        {
            b.ToTable("Passages");
            b.HasKey(e => e.Identifier);

            b.HasOne(e => e.Card).WithMany();
            b.HasOne(e => e.Gate).WithMany();
        });

        builder.Entity<Payment>(b =>
        {
            b.ToTable("Payments");
            b.HasKey(e => e.Identifier);
        });

        builder.Entity<PriceListItem>(b =>
        {
            b.ToTable("PriceListItems");
            b.HasKey(e => e.Identifier);

            b.HasData(new
            {
                Identifier = 1,
                Name = "2h",
                Price = 20D,
                Hours = 2,
            });
            b.HasData(new
            {
                Identifier = 2,
                Name = "3h",
                Price = 40D,
                Hours = 3,
            });
            b.HasData(new
            {
                Identifier = 3,
                Name = "4h",
                Price = 60D,
                Hours = 4,
            });
            b.HasData(new
            {
                Identifier = 4,
                Name = "1d",
                Price = 80D,
                Hours = 1 * 24,
            });
            b.HasData(new
            {
                Identifier = 5,
                Name = "2d",
                Price = 100D,
                Hours = 2 * 24,
            });
            b.HasData(new
            {
                Identifier = 6,
                Name = "3d",
                Price = 120D,
                Hours = 3 * 24,
            });
            b.HasData(new
            {
                Identifier = 7,
                Name = "4d",
                Price = 140D,
                Hours = 4 * 24,
            });
            b.HasData(new
            {
                Identifier = 8,
                Name = "5d",
                Price = 160D,
                Hours = 5 * 24,
            });
            b.HasData(new
            {
                Identifier = 9,
                Name = "6d",
                Price = 180D,
                Hours = 6 * 24,
            });
            b.HasData(new
            {
                Identifier = 10,
                Name = "7d",
                Price = 200D,
                Hours = 7 * 24,
            });
        });
    }
}