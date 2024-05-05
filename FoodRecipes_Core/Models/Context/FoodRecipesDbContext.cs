using FoodRecipes_Core.Models.Entites;
using FoodRecipes_Core.Models.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipes_Core.Models.Context
{
    public class FoodRecipesDbContext : DbContext
    {
        public FoodRecipesDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AdminEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClientEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DishEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DonationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FoodSectionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LoginEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewEntityTypeConfiguration());


        }


        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Dish> Dishes { get; set; }   
        public virtual DbSet<Donation> Donations { get; set; }
        public virtual DbSet<FoodSection> FoodSections { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }

    }

}
