using Back_End_Dot_Net.Models;
using Microsoft.EntityFrameworkCore;

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
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Laptop>()
                .HasOne(laptop => laptop.CPU)
                .WithMany(cpu => cpu.Laptops)
                .HasForeignKey(laptop => laptop.CpuId);
        }
    }
}
