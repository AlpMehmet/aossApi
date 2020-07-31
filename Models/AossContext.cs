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

        public DbSet<AossSorular> AossSorular   { get; set; }
        public DbSet<AossOnlineSinav> AossOnlineSinav   { get; set; }
        public DbSet<AossOnlineSinavSorular> AossOnlineSinavSorular   { get; set; }
        public DbSet<AossZorlukPuanlama> AossZorlukPuanlama   { get; set; }
        public DbSet<AossOnlineSinavOgrenci> AossOnlineSinavOgrenci   { get; set; }
        public DbSet<AossHataliSorular> AossHataliSorular   { get; set; }
    }
}