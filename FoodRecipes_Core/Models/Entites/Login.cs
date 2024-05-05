using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipes_Core.Models.Entites
{
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int LoginId {  get; set; }
        public string ApiKey { get; set; }
        public virtual Admin? Admin { get; set; }
        public virtual Client? Client { get; set; }

    }
}
