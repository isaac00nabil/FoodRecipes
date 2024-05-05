using FoodRecipes_Core.Dtos.Donation;
using FoodRecipes_Core.Dtos.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipes_Core.Dtos.Client
{
    public class ClientRecordDTO
    {
        public int ClientId { get; set; }
        public string ProfileImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public List<DonationRecordDTO> Donations {  get; set; }
        public List<ReviewRecordDTO> Reviews { get; set; }
    } 
}
