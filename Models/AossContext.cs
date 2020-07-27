using Microsoft.EntityFrameworkCore;
using AossAPI.Models;

namespace AossApi.Models
{
    public class AossContext : DbContext
    {
        public AossContext(DbContextOptions<AossContext> options)
            : base(options)
        {
            
        }

        public DbSet<AossHoca> AossHoca { get; set; }
        public DbSet<AossOgrenci> AossOgrenci { get; set; }
        public DbSet<AossYonetici> AossYonetici { get; set; }

    }
}