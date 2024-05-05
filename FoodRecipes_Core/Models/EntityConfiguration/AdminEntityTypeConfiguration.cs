using FoodRecipes_Core.Models.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipes_Core.Models.EntityConfiguration
{
    public class AdminEntityTypeConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(a => a.AdminId);

            builder.Property(a => a.AdminId).UseIdentityColumn();

            builder.Property(a => a.ProfileImagePath).IsRequired(false);
            builder.Property(a => a.DisplayName).IsRequired();
            builder.Property(a => a.Username).IsRequired();
            builder.Property(a => a.Password).IsRequired();
            builder.Property(a => a.UserType).IsRequired(false);


            builder.ToTable(a => a.HasCheckConstraint("CH_Username_Length", "LEN(Username)>=3 AND LEN(Username)<=254"));
            builder.ToTable(a => a.HasCheckConstraint("CH_DisplayName_Length", "LEN(DisplayName)>=3 AND LEN(DisplayName)<=50"));
            builder.ToTable(a => a.HasCheckConstraint("CH_Password_Length", "LEN([Password])>=8 AND LEN([Password])<=20"));

            builder.HasIndex(a => a.Username).IsUnique();


            builder.Property(a => a.CreationDateTime).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(a => a.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.Property(a => a.UserType).IsRequired().HasDefaultValue("Admin");




        }
    }
}
