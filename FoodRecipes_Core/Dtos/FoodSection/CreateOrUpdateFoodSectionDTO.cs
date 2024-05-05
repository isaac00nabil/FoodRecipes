using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FoodRecipes_Core.Helper.Enums.FoodRecipesLookups;

namespace FoodRecipes_Core.Dtos.FoodSection
{
    public class CreateOrUpdateFoodSectionDTO
    {
        
        public int? FoodSectionId { get; set; }
        public string SectionName { get; set; }
        public string Description { get; set; }
    }
}
