using FoodRecipes_Core.Dtos.Account;
using FoodRecipes_Core.Dtos.Dish;
using FoodRecipes_Core.Dtos.Donation;
using FoodRecipes_Core.Dtos.Login;
using FoodRecipes_Core.Dtos.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipes_Core.IRepositories
{
    public interface ISharedRepositoryInterface
    {

        Task<string> CreateDish(CreateOrUpdateDishDTO dto);
        Task<string> CreateDonationType(CreateOrUpdateDonationTypeDTO dto);

        Task<List<DishRecordDTO>> GetAllDishRecord();
        Task<DishRecordDTO> GetDishRecordById(int dishId);
        Task<DishRecordDTO> GetDishRecordByName(string dishName);
        Task<List<ReviewRecordDTO>> GetAllReviewRecord();
        Task<List<DonationRecordDTO>> GetAllDonationRecord();


        Task<string> UpdateDish(CreateOrUpdateDishDTO dto, bool isAdmin = false);
        Task<int> ResetPassword(ResetPasswordDTO dto);

        Task<int> Login(LoginRequestDTO dto);
        Task<int> Logout(int id);

        Task<int> DeleteDish(int dishId, bool isAdmin = false);
        Task<int> DeleteReview(int reviewId, bool isAdmin = false);
        Task<int> DeleteAccount(int clientId);

    }
}
