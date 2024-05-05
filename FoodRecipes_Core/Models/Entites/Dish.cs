using FoodRecipes_Core.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipes_Core.Models.Entites
{
    public class Dish : ParentEntity
    {
        public int DishId { get; set; }
        public string DishImagePath { get; set; }
        public string DishName { get; set; }
        public string Description { get; set; }
        public string Steps { get; set; }
        public virtual FoodSection FoodSection { get; set; }
        public virtual Admin Admin { get; set; }

    }
}
