using FoodRecipes_Core.Models.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipes_Core.Models.EntityConfiguration
{
    public class DishEntityTypeConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.HasKey(d => d.DishId);

            builder.Property(d => d.DishId).UseIdentityColumn();

            builder.Property(d => d.DishImagePath).IsRequired();
            builder.Property(d => d.DishName).IsRequired();
            builder.Property(d => d.Description).IsRequired();
            builder.Property(d => d.Steps).IsRequired();

            builder.HasIndex(d => d.DishName).IsUnique();

            builder.ToTable(d => d.HasCheckConstraint("CH_DishName_Length", "LEN(DishName)>=2 AND LEN(DishName)<=30"));
            builder.ToTable(d => d.HasCheckConstraint("CH_Description_Length", "LEN(Description)>=50 AND LEN(Description)<=255"));
            builder.ToTable(d => d.HasCheckConstraint("CH_Steps_Length", "LEN(Steps)>=30 AND LEN(Steps)<=255"));

            builder.Property(d => d.CreationDateTime).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(d => d.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.HasOne(c => c.FoodSection).WithMany(r => r.Dishes).OnDelete(DeleteBehavior.NoAction);

            //builder.Property(d => d.DishName).IsRequired().HasMaxLength(30).IsFixedLength();
        }

    }
}
