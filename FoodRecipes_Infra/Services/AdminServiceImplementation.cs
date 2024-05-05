using FoodRecipes_Core.Dtos.Admin;
using FoodRecipes_Core.Dtos.Client;
using FoodRecipes_Core.Dtos.Donation;
using FoodRecipes_Core.Dtos.FoodSection;
using FoodRecipes_Core.Dtos.Review;
using FoodRecipes_Core.IRepositories;
using FoodRecipes_Core.IServices;
using FoodRecipes_Core.Models.Entites;
using System.Net;
using static FoodRecipes_Core.Helper.Enums.FoodRecipesLookups;

namespace FoodRecipes_Infra.Services
{

    public class AdminServiceImplementation : IAdminServiceInterface
    {
        private readonly IAdminRepositoryInterface _adminRepositoryInterface;
        public AdminServiceImplementation(IAdminRepositoryInterface adminRepositoryInterface)
        {
            _adminRepositoryInterface = adminRepositoryInterface;
        }

        public async Task<string> CreateAdminAccount(CreateOrUpdateAdminAccountDTO dto)
        {
            var createRepos = await _adminRepositoryInterface.CreateAdminAccount(dto);
            if (createRepos == "200")
            {
                return "Admin account successfully created";
            }
            else
            {
                return "Failed to create admin account: " + createRepos;
            }

        }



        public async Task<string> CreateNewFoodSection(CreateOrUpdateFoodSectionDTO dto)
        {
            var foodSection = await _adminRepositoryInterface.CreateNewFoodSection(dto);
            if (foodSection == "200")
            {
                return "Food section successfully created";
            }
            else
            {
                return $"Failed to create new food section: {foodSection}";
            }
        }


        public async Task<string> DeleteAdminAccount(int adminId)
        {
            var deleteAdmin = await _adminRepositoryInterface.DeleteAdminAccount(adminId);
            if (deleteAdmin == 200)
            {
                return $"Admin with ID: {adminId} successfully deleted";
            }
            else
            {
                return $"{deleteAdmin}: Admin with ID: {adminId} not found";
            }
        }
        public async Task<string> DeleteDonationTypeByCardType(string cardType)
        {
            var deleteCard = await _adminRepositoryInterface.DeleteDonationTypeByCardType(cardType);
            if (deleteCard == HttpStatusCode.OK)
            {
                return $"Donation with string {cardType} has been deleted";
            }
            else
            {
                return $"Donation with string {cardType} not found";
            }
        }

        public async Task<string> DeleteDonationTypeById(int donationId)
        {
            var deleteCard = await _adminRepositoryInterface.DeleteDonationTypeById(donationId);
            if (deleteCard == HttpStatusCode.OK)
            {
                return $"Donation with ID {donationId} has been deleted";

            }
            else
            {
                return $"Donation with ID {donationId} not found";
            }
        }

        public async Task<string> DeleteFoodSectionById(int foodSectionId)
        {
            var deleteFoodSectio = await _adminRepositoryInterface.DeleteFoodSectionById(foodSectionId);

            if (deleteFoodSectio == 200)
            {
                return $"Food section with ID {foodSectionId} has been deleted";

            }
            else
            {
                return $"Food section with ID {foodSectionId} not found";
            }

        }

        public async Task<List<ClientRecordDTO>> GetAllClient()
        {
            var allClient = await _adminRepositoryInterface.GetAllClient();
            if (allClient != null && allClient.Count > 0)
            {
                // return clients list
                return allClient;
            }
            else
            {
                // return an empty list if there are no clients
                return null;
            }

        }

        public async Task<List<DonationRecordDTO>> GetAllDonationRecord()
        {
            var allDonation = await _adminRepositoryInterface.GetAllDonationRecord();
            if (allDonation?.Count > 0)
            {
                // return donations list
                return allDonation;
            }
            else
            {
                // return an empty list if there are no donations
                return null;
            }
        }

        public async Task<ClientRecordDTO> GetClientById(int clientId)
        {
            // try to retrieve a client record by ID from the repository.
            // if the client is not found, return a new instance of ClientRecordDTO.
            return await _adminRepositoryInterface.GetClientById(clientId) ?? null;

        }

        public async Task<ClientRecordDTO> GetClientByUsername(string username)
        {
            // try to retrieve a client record by username from the repository.
            // if the client is not found, return a new instance of ClientRecordDTO.
            return await _adminRepositoryInterface.GetClientByUsername(username) ?? null;
        }

        public async Task<List<DonationRecordDTO>> GetDonationRecordByCardType(string cardType)
        {
            var donationRecords = await _adminRepositoryInterface.GetDonationRecordByCardType(cardType);
            if (donationRecords != null)
            {
                // return donations list
                return donationRecords;
            }
            else
            {
                // return an empty list if there are no donations
                return null;
            }
        }

        public async Task<DonationRecordDTO> GetDonationRecordById(int donationId)
        {
            // try to retrieve a donation record by ID from the repository.
            // if the donation is not found, return a new instance of DonationRecordDTO.
            return await _adminRepositoryInterface.GetDonationRecordById(donationId) ?? null;
        }

        public async Task<List<ReviewRecordDTO>> GetReviewRecordByClientId(int clientId)
        {
            var reviewRecord = await _adminRepositoryInterface.GetReviewRecordByClientId(clientId);
            if (reviewRecord?.Count > 0)
            {
                // return reviews list
                return reviewRecord;
            }
            else
            {
                // return an empty list if there are no reviews
                return null;
            }
        }

        public async Task<ReviewRecordDTO> GetReviewRecordById(int reviewId)
        {
            // try to retrieve a eview record by ID from the repository.
            // if the retrieve is not found, return a new instance of ReviewRecordDTO.
            return await _adminRepositoryInterface.GetReviewRecordById(reviewId) ?? null;
        }



        public async Task<HttpStatusCode> UpdateDonationType(CreateOrUpdateDonationTypeDTO dto)
        {
            var updateDonation = await _adminRepositoryInterface.UpdateDonationType(dto);
            switch (updateDonation)
            {
                case 404:
                    return HttpStatusCode.NotFound;
                case 200:
                    return HttpStatusCode.OK;
                default:
                    return HttpStatusCode.InternalServerError;
            }



        }

        public async Task<string> UpdateFoodSection(CreateOrUpdateFoodSectionDTO dto)
        {
            var foodSection = await _adminRepositoryInterface.UpdateFoodSection(dto);

            switch (foodSection)
            {
                case HttpStatusCode.NotFound:
                    return "Food Section not found";
                case HttpStatusCode.OK:
                    return "Food Section updated successfully";
                default:
                    return $"Failed to update Food Section updated: {foodSection}";

            }
        }
    }
}
