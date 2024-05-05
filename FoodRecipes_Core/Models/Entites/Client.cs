using FoodRecipes_Core.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipes_Core.Models.Entites
{
    public class Client : ParentEntity
    {
        public int ClientId { get; set; }
        public string ProfileImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string UserType { get; set; } 
        public string Password { get; set; }
        public string Email { get; set; }
        public virtual Login Login { get; set; }
        public virtual Review Review { get; set; }
        public virtual ICollection<Donation> Donations { get; set; }
    }
}
