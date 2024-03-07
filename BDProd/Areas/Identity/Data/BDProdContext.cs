using BDProd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BDProd.Data;

public class BDProdContext : IdentityDbContext<IdentityUser>
{
    public BDProdContext(DbContextOptions<BDProdContext> options)
        : base(options)
    {
    }

    public DbSet<Image> Images { get; set; }
    public DbSet<RefLabo> RefLabos { get; set; }
    public DbSet<RefProd> RefProds { get; set; }
    public DbSet<HistoImg> HistoImgs { get; set; }
    public DbSet<PGroup> PGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<PGroup>()
                .HasKey(pg => new { pg.REF_ID1, pg.REF_ID2 });
    }
}
