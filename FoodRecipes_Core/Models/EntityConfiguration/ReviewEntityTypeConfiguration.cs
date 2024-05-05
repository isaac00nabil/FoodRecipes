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
    public class ReviewEntityTypeConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.ReviewId);

            builder.Property(r => r.ReviewId).UseIdentityColumn();

            builder.Property(r => r.Rating).IsRequired();
            builder.Property(r => r.Comment).IsRequired(false).HasMaxLength(255);

            builder.ToTable(r => r.HasCheckConstraint("CH_Rating_Range", "Rating>=0 AND Rating<=10"));

            builder.Property(r => r.Rating).HasDefaultValue(0);

            builder.Property(r => r.CreationDateTime).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(r => r.IsDeleted).IsRequired().HasDefaultValue(false);

            //builder.HasOne(c => c.Client).WithOne(r => r.Review).OnDelete(DeleteBehavior.NoAction);

            

        }
    }
}
