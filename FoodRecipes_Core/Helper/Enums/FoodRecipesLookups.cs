using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipes_Core.Helper.Enums
{
    public static class FoodRecipesLookups
    {

        public enum PaymentMethod
        {
            Visa,
            PayPal
        }

        public enum FoodSection
        {
            ArabicFood = 10,
            EastAsianFood,
            EuropeanFood,
            FastFood,
            SeaFood,
            Snacks,
            Sweets
        }

        public enum CardType
        {
            Bronze = 100,
            Silver,
            Gold,
            Diamond,
            Platinum
        }

        public enum UserType
        {
            Admin = 10,
            Client
        }

    }
}


