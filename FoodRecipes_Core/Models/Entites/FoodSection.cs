using FoodRecipes_Core.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FoodRecipes_Core.Helper.Enums.FoodRecipesLookups;

namespace FoodRecipes_Core.Models.Entites
{
    public class FoodSection : ParentEntity
    {
        public int FoodSectionId { get; set; }
        public string SectionName { get; set; }
        public string Description { get; set; }
        public virtual Admin Admin { get; set; }
        public virtual ICollection<Dish> Dishes { get; set; }

    }
}
