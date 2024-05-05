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
    public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.ClientId);

            builder.Property(c => c.ClientId).UseIdentityColumn();
            builder.Property(r => r.ClientId).ValueGeneratedOnAdd();

            builder.Property(c => c.ProfileImagePath).IsRequired(false);
            builder.Property(c => c.FirstName).IsRequired();
            builder.Property(c => c.LastName).IsRequired();
            builder.Property(c => c.Username).IsRequired();
            builder.Property(c => c.Password).IsRequired();
            builder.Property(c => c.Email).IsRequired();
            builder.Property(c => c.UserType).IsRequired();

            builder.HasIndex(c => c.Username).IsUnique();
            builder.HasIndex(c => c.Email).IsUnique();

            builder.ToTable(c => c.HasCheckConstraint("CH_FirstName_Length", "LEN(FirstName)>=3 AND LEN(FirstName)<=20"));
            builder.ToTable(c => c.HasCheckConstraint("CH_LastName_Length", "LEN(LastName)>=3 AND LEN(LastName)<=20"));
            builder.ToTable(c => c.HasCheckConstraint("CH_Username_Length", "LEN(Username)>=3 AND LEN(Username)<=50"));
            builder.ToTable(c => c.HasCheckConstraint("CH_Password_Length", "LEN([Password])>=8 AND LEN([Password])<=20"));
            builder.ToTable(c =>
            {
                c.HasCheckConstraint("CH_Email_Like", "Email Like '%@gmail.com%' OR Email Like  '%@outlook.com%' OR Email Like '%@yahoo.com%'");
                c.HasCheckConstraint("CH_Email_Length", "LEN(Email)>=13 AND LEN(Email)<=254");
            });

            builder.Property(c => c.CreationDateTime).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.Property(c => c.UserType).IsRequired().HasDefaultValue("Client");

        }
    }
}
