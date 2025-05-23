using API_Interna.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Interna.Data
{
    public class DPContext : DbContext
    {
        public DbSet<OSC> OSCs { get; set; }
        public DbSet<ContatoOSC> ContatoOSCs { get; set; }
        public DbSet<LocalizacaoOSC> LocalizacoesOSCs { get; set; }
        public DbSet<RepresentanteLegal> RepresentanteLegais { get; set; }
        public DbSet<Edital> Editais { get; set; }
        public DbSet<Termo> Termos { get; set; }
        public DbSet<LocalizacaoTermo> LocalizacoesTermos { get; set; }
        public DbSet<AditivoApostilamento> AditivosApostilamentos { get; set; }
        public DbSet<Alteracao> Alteracoes { get; set; }

        public DPContext(DbContextOptions<DPContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dp_teste_01") ;
        }
    }
}
