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
    public class FoodSectionEntityTypeConfiguration : IEntityTypeConfiguration<FoodSection>
    {
        public void Configure(EntityTypeBuilder<FoodSection> builder)
        {
            builder.HasKey(f => f.FoodSectionId);


            builder.Property(f => f.FoodSectionId).UseIdentityColumn();

            builder.Property(f => f.SectionName).IsRequired();
            builder.Property(f => f.Description).IsRequired().HasMaxLength(255).IsFixedLength();

            builder.ToTable(f => f.HasCheckConstraint("CH_SectionName_NameOfSection", "LEN(SectionName)>=10 AND LEN(SectionName)<=16"));

            builder.HasIndex(f => f.SectionName).IsUnique();

            builder.Property(f => f.CreationDateTime).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(f => f.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.HasOne(c => c.Admin).WithOne(c => c.FoodSection).HasForeignKey("FoodSection", "AdminId");


        }
    }
}
