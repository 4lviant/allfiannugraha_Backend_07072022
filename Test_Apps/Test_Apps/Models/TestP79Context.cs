using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Test_Apps.Models
{
    public partial class TestP79Context : DbContext
    {
       

        public TestP79Context(DbContextOptions<TestP79Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Nasabah> Nasabahs { get; set; }
        public virtual DbSet<Transaksi> Transaksis { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Nasabah>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.ToTable("Nasabah");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Transaksi>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.ToTable("Transaksi");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DebitCreditStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Transaksis)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaksi_Nasabah");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
