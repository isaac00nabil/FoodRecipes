using FoodRecipes_Core.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipes_Core.Models.Entites
{
    public class Review : ParentEntity
    {
        public int ReviewId {  get; set; }
        public float Rating { get; set; }
        public string Comment { get; set; }
        public int ClientId { get; set; }
        public virtual Login Login { get; set; }
        public virtual Client Client { get; set; }
    }
}
