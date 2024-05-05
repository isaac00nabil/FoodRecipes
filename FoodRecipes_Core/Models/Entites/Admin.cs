using FoodRecipes_Core.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipes_Core.Models.Entites
{
    public class Admin : ParentEntity
    {
        public int AdminId { get; set; }
        public string ProfileImagePath { get; set; }
        public string UserType { get; set; }
        public string Username { get; set; }
        public string DisplayName {  get; set; }
        public string Password { get; set; }
        public virtual Login Login { get; set; }
        public virtual FoodSection FoodSection { get; set; }
        public virtual ICollection<Dish> Dishes { get; set; }


    }
}
