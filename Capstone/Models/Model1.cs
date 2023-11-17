using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Capstone.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<CategoriaGiochi> CategoriaGiochi { get; set; }
        public virtual DbSet<DettaglioOrdine> DettaglioOrdine { get; set; }
        public virtual DbSet<Giochi> Giochi { get; set; }
        public virtual DbSet<Ordine> Ordine { get; set; }
        public virtual DbSet<OrdineGioco> OrdineGioco { get; set; }
        public virtual DbSet<RecensioneGioco> RecensioneGioco { get; set; }
        public virtual DbSet<StatisticheSito> StatisticheSito { get; set; }
        public virtual DbSet<Utenti> Utenti { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaGiochi>()
                .HasMany(e => e.Giochi)
                .WithRequired(e => e.CategoriaGiochi)
                .HasForeignKey(e => e.CategoriaId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DettaglioOrdine>()
                .Property(e => e.PrezzoUnitario)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Giochi>()
                .Property(e => e.Prezzo)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Giochi>()
                .HasMany(e => e.DettaglioOrdine)
                .WithRequired(e => e.Giochi)
                .HasForeignKey(e => e.GiocoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Giochi>()
                .HasMany(e => e.OrdineGioco)
                .WithRequired(e => e.Giochi)
                .HasForeignKey(e => e.GiocoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Giochi>()
                .HasMany(e => e.RecensioneGioco)
                .WithRequired(e => e.Giochi)
                .HasForeignKey(e => e.GiocoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ordine>()
                .Property(e => e.TotaleOrdine)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Ordine>()
                .HasMany(e => e.DettaglioOrdine)
                .WithRequired(e => e.Ordine)
                .HasForeignKey(e => e.OrdineId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utenti>()
                .HasMany(e => e.Giochi)
                .WithRequired(e => e.Utenti)
                .HasForeignKey(e => e.UtenteId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utenti>()
                .HasMany(e => e.Ordine)
                .WithRequired(e => e.Utenti)
                .HasForeignKey(e => e.UtenteId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utenti>()
                .HasMany(e => e.OrdineGioco)
                .WithRequired(e => e.Utenti)
                .HasForeignKey(e => e.UtenteId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utenti>()
                .HasMany(e => e.RecensioneGioco)
                .WithRequired(e => e.Utenti)
                .HasForeignKey(e => e.UtenteId)
                .WillCascadeOnDelete(false);
        }
    }
}
