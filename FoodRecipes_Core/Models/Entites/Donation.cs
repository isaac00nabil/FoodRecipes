using FoodRecipes_Core.Models.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FoodRecipes_Core.Helper.Enums.FoodRecipesLookups;

namespace FoodRecipes_Core.Models.Entites
{
    public class Donation : ParentEntity
    {
        public int DonationId {  get; set; }
        public string CardType { get; set; }
        public float Price { get; set; }
        public int Point { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public virtual Client Client { get; set; }
    }
}
