using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FoodRecipes_Core.Helper.Enums.FoodRecipesLookups;

namespace FoodRecipes_Core.Dtos.Donation
{
    public class DonationRecordDTO
    {
        public int DonationId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int Point { get; set; }
        public string CardType { get; set; }
        public float Price { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
