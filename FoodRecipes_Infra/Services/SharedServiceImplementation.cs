using FoodRecipes_Core.Dtos.Account;
using FoodRecipes_Core.Dtos.Dish;
using FoodRecipes_Core.Dtos.Donation;
using FoodRecipes_Core.Dtos.Login;
using FoodRecipes_Core.Dtos.Review;
using FoodRecipes_Core.IRepositories;
using FoodRecipes_Core.IServices;
using FoodRecipes_Core.Models.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipes_Infra.Services
{
    public class SharedServiceImplementation : ISharedServiceInterface
    {

        private readonly ISharedRepositoryInterface _sharedRepositoryInterface;
        public SharedServiceImplementation(ISharedRepositoryInterface sharedRepositoryInterface)
        {
            _sharedRepositoryInterface = sharedRepositoryInterface;
        }


        #region Donation Management

        #region Create
        public async Task<string> CreateDonationType(CreateOrUpdateDonationTypeDTO dto)
        {
            var donationType = await _sharedRepositoryInterface.CreateDonationType(dto);
            if (donationType == "200")
            {
                return $"DonationType successfully created";
            }
            else
            {
                return "Failed to create new DonationType: " + donationType;
            }
        }
        #endregion Create

        #region Read
        public async Task<List<DonationRecordDTO>> GetAllDonationRecord()
        {
            var donation = await _sharedRepositoryInterface.GetAllDonationRecord();
            if (donation?.Count > 0)
            {
                // return donations list
                return donation;
            }
            else
            {
                // return an empty list if there are no donations
                return null;
            }
        }
        #endregion Read

        #endregion Donation Management

        #region Dish Management

        #region Create
        public async Task<string> CreateDish(CreateOrUpdateDishDTO dto)
        {
            var dish = await _sharedRepositoryInterface.CreateDish(dto);
            if (dish == "200")
            {
                return "Dish successfully created";
            }
            else
            {
                return "Failed to create new dish" + dish;
            }
        }
        #endregion Create

        #region Read
        public async Task<List<DishRecordDTO>> GetAllDishRecord()
        {
            var dish = await _sharedRepositoryInterface.GetAllDishRecord();
            if (dish?.Count > 0)
            {
                // return dishes list
                return dish;
            }
            else
            {
                // return an empty list if there are no dishes
                return null;
            }
        }
        public async Task<DishRecordDTO> GetDishRecordById(int dishId)
        {
            var dish = await _sharedRepositoryInterface.GetDishRecordById(dishId);
            if (dish.ToString().IsNullOrEmpty() == false)
            {
                // return dish
                return dish;
            }
            else
            {
                // return an empty
                return new DishRecordDTO();
            }
        }

        public async Task<DishRecordDTO> GetDishRecordByName(string dishName)
        {
            var dish = await _sharedRepositoryInterface.GetDishRecordByName(dishName);
            if (dish.ToString().IsNullOrEmpty() == false)
            {
                // return dish
                return dish;
            }
            else
            {
                // return an empty
                return null;
            }
        }
        #endregion Read

        #region Update
        public async Task<string> UpdateDish(CreateOrUpdateDishDTO dto, bool isAdmin = false)
        {
            var dish = await _sharedRepositoryInterface.UpdateDish(dto);
            if (dish.Contains("not found"))
            {
                return "Dish not found";
            }
            else if (dish.Contains("successfully"))
            {
                return "Dish updated successfully";
            }
            else
            {
                return "Invalid request";
            }
        }
        #endregion Update

        #region Delete
        public async Task<string> DeleteDish(int dishId, bool isAdmin = false)
        {
            var dish = await _sharedRepositoryInterface.DeleteDish(dishId);
            if (dish == 200)
            {
                return $"Dish with ID: {dishId} successfully deleted";
            }
            else
            {
                return $"{dish}: Dish with ID: {dishId} not found";
            }

        }
        #endregion Delete

        #endregion Dish Management

        #region Account Management

        #region Login
        public async Task<string> Login(LoginRequestDTO dto)
        {
            var login = await _sharedRepositoryInterface.Login(dto);
            switch (login)
            {
                case 200:
                    return "Login successful";
                case 404:
                    return "User not found";
                default:
                    return "Invalid Data";
            }
        }




        #endregion Login

        #region Logout
        public async Task<string> Logout(int id)
        {
            var logout = await _sharedRepositoryInterface.Logout(id);
            if (logout == 200)
            {
                return "Logout successful";
            }
            else
            {
                return "Logout failed";
            }
            
        }
        #endregion Logout

        #region ResetPassword
        public async Task<string> ResetPassword(ResetPasswordDTO dto)
        {
            var resetPassword = await _sharedRepositoryInterface.ResetPassword(dto);
            if (resetPassword == 200)
            {
                return "Password reset successfully";
            }
            else if (resetPassword == 400)
            {
                return "Bad request due to password mismatch";
            }
            else
            {
                return "Password reset failed";
            }
        }
        #endregion ResetPassword

        #region Delete
        public async Task<string> DeleteAccount(int clientId)
        {
            var account = await _sharedRepositoryInterface.DeleteAccount(clientId);

            if (account == 200)
            {
                return $"Client with ID: {clientId} successfully deleted";
            }
            else
            {
                return $"{account}: Client with ID: {clientId} not found";
            }
        }
        #endregion Delete

        #endregion Account Management

        #region Review Management

        #region Read

        public async Task<List<ReviewRecordDTO>> GetAllReviewRecord()
        {
            var review = await _sharedRepositoryInterface.GetAllReviewRecord();
            if (review?.Count > 0)
            {
                // return reviews list
                return review;
            }
            else
            {
                // return an empty list if there are no reviews
                return null;
            }
        }
        #endregion Read

        #region Delete
        public async Task<string> DeleteReview(int reviewId, bool isAdmin = false)
        {
            var review = await _sharedRepositoryInterface.DeleteReview(reviewId);
            if (review == 200)
            {
                return $"Review with ID: {reviewId} successfully deleted";
            }
            else
            {
                return $"{review}: Review with ID: {reviewId} not found";
            }

        }
        #endregion Delete

        #endregion Review Management

    }
}
