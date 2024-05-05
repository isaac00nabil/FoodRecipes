
using FoodRecipes_Core.Dtos.Client;
using FoodRecipes_Core.Dtos.Donation;
using FoodRecipes_Core.Dtos.Review;
using FoodRecipes_Core.IRepositories;
using FoodRecipes_Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipes_Infra.Services
{

    public class ClientServiceImplementation : IClientServiceInterface
    {

        private readonly IClientRepositoryInterface _clientRepositoryInterface;
        public ClientServiceImplementation(IClientRepositoryInterface clientRepositoryInterface)
        {
            _clientRepositoryInterface = clientRepositoryInterface;
        }

        #region Client Management

        #region Create
        public async Task<string> CreateAccount(ClientRegistrationDTO dto)
        {
            var account = await _clientRepositoryInterface.CreateAccount(dto);
            if (account == "200")
                return "Client account successfully created";

            else
                return "Failed to create client account: " + account;

        }
        #endregion Create

        #region Update
        public async Task<string> UpdateAccount(ClientUpdateProfileDTO dto)
        {
            var account = await _clientRepositoryInterface.UpdateAccount(dto);
            if (account.Contains("not found"))
            {
                return "Client not found";
            }
            else if (account.Contains("successfully"))
            {
                return "Client account updated successfully";
            }
            else
            {
                return account;
            }
        }
        #endregion Update

        #endregion Client Management

        #region Review Management

        #region Create
        public async Task<string> CreateNewReview(CreateOrUpdateReviewDTO dto)
        {
            var review = await _clientRepositoryInterface.CreateNewReview(dto);
            if (review == "200")
                return "Review successfully created";

            else
                return "Failed to create review: " + review;

        }
        #endregion Create

        #region Update
        public async Task<string> UpdateReview(CreateOrUpdateReviewDTO dto)
        {
            var review = await _clientRepositoryInterface.UpdateReview(dto);
            if (review.Contains("not found"))
            {
                return "Review not found";
            }
            else if (review.Contains("successfully"))
            {
                return "Review updated successfully";
            }
            else
            {
                return review;
            }
        }
        #endregion Update

        #endregion Review Management

        #region Donation Management

        #region Read
        public async Task<List<DonationRecordDTO>> GetDonationRecordByClientId(int clientId)
        {
            var donationList = await _clientRepositoryInterface.GetDonationRecordByClientId(clientId);
            if (donationList?.Count > 0)
                return donationList;
            else
                return null;
        }

        #endregion Read

        #endregion Donation Management

    }
}
