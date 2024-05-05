using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipes_Core.Dtos.Dish
{
    public class DishRecordDTO
    {
        public int DishId { get; set; }
        public string DishImagePath { get; set; }
        public string DishName { get; set; }
        public string Description { get; set; }
        public string Steps { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
