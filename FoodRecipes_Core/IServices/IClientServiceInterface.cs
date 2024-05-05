using FoodRecipes_Core.Dtos.Client;
using FoodRecipes_Core.Dtos.Donation;
using FoodRecipes_Core.Dtos.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipes_Core.IServices
{
    public interface IClientServiceInterface
    {
        Task<string> CreateAccount(ClientRegistrationDTO dto);
        Task<string> CreateNewReview(CreateOrUpdateReviewDTO dto);

        Task<List<DonationRecordDTO>> GetDonationRecordByClientId(int clientId);

        Task<string> UpdateReview(CreateOrUpdateReviewDTO dto);
        Task<string> UpdateAccount(ClientUpdateProfileDTO dto);

    }
}
