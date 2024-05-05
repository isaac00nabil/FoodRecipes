using FoodRecipes_Core.Dtos.Admin;
using FoodRecipes_Core.Dtos.Client;
using FoodRecipes_Core.Dtos.Donation;
using FoodRecipes_Core.Dtos.FoodSection;
using FoodRecipes_Core.Dtos.Review;
using FoodRecipes_Core.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static FoodRecipes_Core.Helper.Enums.FoodRecipesLookups;

namespace FoodRecipes_Core.IServices
{
    public interface IAdminServiceInterface
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


        Task<HttpStatusCode> UpdateDonationType(CreateOrUpdateDonationTypeDTO dto);
        Task<string> UpdateFoodSection(CreateOrUpdateFoodSectionDTO dto);


        Task<string> DeleteAdminAccount(int adminId);
        Task<string> DeleteDonationTypeById(int donationId);
        Task<string> DeleteDonationTypeByCardType(string cardType);
        Task<string> DeleteFoodSectionById(int foodSectionId);

    }
}
