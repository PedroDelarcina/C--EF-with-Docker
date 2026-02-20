using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

    public class InfestationNewzDbContext : DbContext
    {
    public InfestationNewzDbContext(DbContextOptions<InfestationNewzDbContext> options) : base(options)
        {
        }
        public DbSet<Player> Players { get; set; }
        public DbSet<Clan> Clans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            base.OnModelCreating(modelBuilder);

             modelBuilder.Entity<Player>()
             .Property(p => p.Kda)
             .HasPrecision(18, 2);

        // Configurações adicionais, se necessário
    }   
}

