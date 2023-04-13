using Back_End_Dot_Net.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Back_End_Dot_Net.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Image> Images { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<Chipset> Chipsets { get; set; }
        public DbSet<Features> Features { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setting Laptop CPU <---> Laptop relationship
            modelBuilder.Entity<Laptop>()
                .HasOne(laptop => laptop.CPU)
                .WithMany(cpu => cpu.Laptops)
                .HasForeignKey(laptop => laptop.CpuId);

            // Parsing string array -> string for SQL
            var valueComparer = new ValueComparer<IEnumerable<string>>(
                (c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()
            );

            // Chipset Model
            modelBuilder.Entity<Chipset>()
                .Property(e => e.PerformanceFeatures)
                .HasConversion(
                    v => v == null ? null : string.Join(',', v),
                    v => v == null ? null : v.Split(',', StringSplitOptions.RemoveEmptyEntries))
                .Metadata.SetValueComparer(valueComparer);

            // Laptop Model
            modelBuilder.Entity<Laptop>()
            .Property(e => e.PerformanceFeatures)
            .HasConversion(
                v => v == null ? null : string.Join(',', v),
                v => v == null ? null : v.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .Metadata.SetValueComparer(valueComparer);
            modelBuilder.Entity<Laptop>()
            .Property(e => e.ScreenFeatures)
            .HasConversion(
                v => v == null ? null : string.Join(',', v),
                v => v == null ? null : v.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .Metadata.SetValueComparer(valueComparer);
            modelBuilder.Entity<Laptop>()
            .Property(e => e.DesignFeatures)
            .HasConversion(
                v => v == null ? null : string.Join(',', v),
                v => v == null ? null : v.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .Metadata.SetValueComparer(valueComparer);
            modelBuilder.Entity<Laptop>()
            .Property(e => e.Features)
            .HasConversion(
                v => v == null ? null : string.Join(',', v),
                v => v == null ? null : v.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .Metadata.SetValueComparer(valueComparer);

            // Phone Model
            modelBuilder.Entity<Phone>()
            .Property(e => e.PerformanceFeatures)
            .HasConversion(
                v => v == null ? null : string.Join(',', v),
                v => v == null ? null : v.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .Metadata.SetValueComparer(valueComparer);
            modelBuilder.Entity<Phone>()
            .Property(e => e.ScreenFeatures)
            .HasConversion(
                v => v == null ? null : string.Join(',', v),
                v => v == null ? null : v.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .Metadata.SetValueComparer(valueComparer);
            modelBuilder.Entity<Phone>()
            .Property(e => e.DesignFeatures)
            .HasConversion(
                v => v == null ? null : string.Join(',', v),
                v => v == null ? null : v.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .Metadata.SetValueComparer(valueComparer);
            modelBuilder.Entity<Phone>()
            .Property(e => e.Features)
            .HasConversion(
                v => v == null ? null : string.Join(',', v),
                v => v == null ? null : v.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .Metadata.SetValueComparer(valueComparer);
        }
    }
}
