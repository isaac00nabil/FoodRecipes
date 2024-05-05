using FoodRecipes_Core.Dtos.Admin;
using FoodRecipes_Core.Dtos.Client;
using FoodRecipes_Core.Dtos.Donation;
using FoodRecipes_Core.Dtos.FoodSection;
using FoodRecipes_Core.Dtos.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static FoodRecipes_Core.Helper.Enums.FoodRecipesLookups;

namespace FoodRecipes_Core.IRepositories
{
    public interface IAdminRepositoryInterface
    {

        Task<string> CreateAdminAccount(CreateOrUpdateAdminAccountDTO dto);
        Task<string> CreateNewFoodSection(CreateOrUpdateFoodSectionDTO dto);


        Task<List<DonationRecordDTO>> GetAllDonationRecord();
        Task<DonationRecordDTO> GetDonationRecordById(int donationId);
        Task<List<DonationRecordDTO>> GetDonationRecordByCardType(string cardType);
        Task<ClientRecordDTO> GetClientByUsername(string username);
        Task<ClientRecordDTO> GetClientById(int clientId);
        Task<List<ClientRecordDTO>> GetAllClient();
        Task<List<ReviewRecordDTO>> GetReviewRecordByClientId(int clientId);
        Task<ReviewRecordDTO> GetReviewRecordById(int reviewId);


        Task<int> UpdateDonationType(CreateOrUpdateDonationTypeDTO dto);
        Task<HttpStatusCode> UpdateFoodSection(CreateOrUpdateFoodSectionDTO dto);


        Task<int> DeleteAdminAccount(int adminId);
        Task<HttpStatusCode> DeleteDonationTypeById(int donationId);
        Task<HttpStatusCode> DeleteDonationTypeByCardType(string cardType);
        Task<int> DeleteFoodSectionById(int foodSectionId);


    }
}
