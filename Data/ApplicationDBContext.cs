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
    }
}
