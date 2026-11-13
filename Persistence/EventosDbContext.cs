using DSW_3P_PR01.Models;
using Microsoft.EntityFrameworkCore;

namespace DSW_3P_PR01.Persistence;

public class EventosDbContext(DbContextOptions<EventosDbContext> options) : DbContext(options)
{
    public DbSet<Evento> Eventos => Set<Evento>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Evento>().ToTable("Eventos", "dbo", tabla =>
            tabla.HasCheckConstraint("CK_Eventos_CupoMaximo", "[CupoMaximo] > 0"));
    }
}
