using FoodRecipes_Core.Models.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipes_Core.Models.EntityConfiguration
{
    public class LoginEntityTypeConfiguration : IEntityTypeConfiguration<Login>
    {



        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Login> builder)
        {

            builder.HasKey(x => x.LoginId);

            builder.Property(x => x.LoginId).UseIdentityColumn();

            builder.Property(x => x.ApiKey).IsRequired(false);

            builder.HasOne(c => c.Admin).WithOne(c => c.Login).HasForeignKey("Login", "AdminId");
            builder.HasOne(c => c.Client).WithOne(c => c.Login).HasForeignKey("Login", "ClientId");

        }
    }
}
