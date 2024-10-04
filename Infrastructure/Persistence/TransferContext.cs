using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class TransferContext : DbContext
    {
        public TransferContext(DbContextOptions<TransferContext> options) : base(options)
        {
        }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<TransferType> TransferTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.ToTable("Transfer");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();
                //entity.HasOne(t => t.SrcAccount)
                //      .WithMany()
                //      .HasForeignKey(t => t.SrcAccountId);
                //entity.HasOne(t => t.DestAccount)
                //      .WithMany()
                //      .HasForeignKey(t => t.DestAccountId);
                entity.HasOne(t => t.TransferType)
                      .WithMany()
                      .HasForeignKey(t => t.TypeId);
            });
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TransferType>(entity =>
            {
                entity.ToTable("TransferType");
                entity.HasKey(tt => tt.Id);
            });

        }
    }
}
