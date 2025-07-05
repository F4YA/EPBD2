using EPBD2.Models;
using Microsoft.EntityFrameworkCore;

namespace EPBD2
{
    public class EPDbContext : DbContext
    {
        public DbSet<Osc> Oscs { get; set; }
        public DbSet<ContatoOsc> ContatoOscs { get; set; }
        public DbSet<LocalizacaoOsc> LocalizacoesOscs { get; set; }
        public DbSet<RepresentanteLegal> RepresentanteLegais { get; set; }
        public DbSet<Edital> Editais { get; set; }
        public DbSet<Termo> Termos { get; set; }
        public DbSet<LocalizacaoTermo> LocalizacoesTermos { get; set; }
        public DbSet<AditivoApostilamento> AditivosApostilamentos { get; set; }
        public DbSet<Alteracoes> Alteracoes { get; set; }

        public EPDbContext(DbContextOptions<EPDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("epbd2");
        }
    }
}
