namespace HousePlans.Data
{
    using HousePlans.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
  
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Floor> Floors { get; set; }

        public DbSet<House> Houses { get; set; }

        public DbSet<Plan> Plans { get; set; }

        public DbSet<Instalation> Instalations { get; set; }

        public DbSet<Material> Materials { get; set; }

    }
}