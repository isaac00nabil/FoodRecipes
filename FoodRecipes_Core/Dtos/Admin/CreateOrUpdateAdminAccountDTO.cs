using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipes_Core.Dtos.Admin
{
    public class CreateOrUpdateAdminAccountDTO
    {
        public int? AdminId { get; set; }
        public string? ProfileImagePath { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
    }
}
