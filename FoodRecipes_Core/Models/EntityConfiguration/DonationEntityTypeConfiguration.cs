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
    public class DonationEntityTypeConfiguration : IEntityTypeConfiguration<Donation>
    {
        public void Configure(EntityTypeBuilder<Donation> builder)
        {
            builder.HasKey(d => d.DonationId);

            builder.Property(d => d.DonationId).UseIdentityColumn();

            builder.Property(d => d.PaymentMethod).IsRequired();
            builder.Property(d => d.Point).IsRequired();
            builder.Property(d => d.CardType).IsRequired();
            builder.Property(d => d.Price).IsRequired();

            builder.ToTable(d => d.HasCheckConstraint("CH_PaymentMethod_Type", "PaymentMethod>=0 AND PaymentMethod<=1 "));
            builder.ToTable(d => d.HasCheckConstraint("CH_CardType", "LEN(CardType)>=100 AND LEN(CardType)<=104 "));
            builder.ToTable(d => d.HasCheckConstraint("CH_Point_Count", "Point>=1"));
            builder.ToTable(d => d.HasCheckConstraint("CH_Price_Value", "Price>0"));

            builder.Property(d => d.CardType).HasMaxLength(50).IsFixedLength();

            builder.HasIndex(d => d.CardType).IsUnique();

            builder.Property(d => d.CreationDateTime).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(d => d.IsDeleted).IsRequired().HasDefaultValue(false);

        }
    }
}
