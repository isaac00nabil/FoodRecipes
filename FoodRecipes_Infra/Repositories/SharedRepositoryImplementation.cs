using FoodRecipes_Core.Dtos.Account;
using FoodRecipes_Core.Dtos.Dish;
using FoodRecipes_Core.Dtos.Donation;
using FoodRecipes_Core.Dtos.Login;
using FoodRecipes_Core.Dtos.Review;
using FoodRecipes_Core.IRepositories;
using FoodRecipes_Core.Models.Context;
using FoodRecipes_Core.Models.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FoodRecipes_Core.Helper.Enums.FoodRecipesLookups;

namespace FoodRecipes_Infra.Repositories
{
    public class SharedRepositoryImplementation : ISharedRepositoryInterface
    {

        private readonly FoodRecipesDbContext _dbContext;
        public SharedRepositoryImplementation(FoodRecipesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Dish Management

        #region Create
        public async Task<string> CreateDish(CreateOrUpdateDishDTO dto)
        {
            try
            {
                Dish dish = new Dish();
                dish.DishImagePath = dto.DishImagePath;
                dish.DishName = dto.DishName;
                dish.Description = dto.Description;
                dish.Steps = dto.Steps;

                await _dbContext.AddAsync(dish);
                await _dbContext.SaveChangesAsync();

                return ("200"); // Ok
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the dish creation process
                return ex.Message;
            }
        }
        #endregion Create

        #region Read



        public async Task<DishRecordDTO> GetDishRecordByName(string dishName)
        {
            var dishRecords = await _dbContext.Dishes.Where(d => d.DishName == dishName).Select(d => new DishRecordDTO
            {
                DishId = d.DishId,
                DishImagePath = d.DishImagePath,
                DishName = d.DishName,
                Description = d.Description,
                Steps = d.Steps,
                CreationDateTime = d.CreationDateTime,
                IsDeleted = d.IsDeleted
            }).FirstOrDefaultAsync();

            // Return the dish records if available, otherwise return an empty
            return dishRecords;
        }

        public async Task<DishRecordDTO> GetDishRecordById(int dishId)
        {
            var dishRecords = await _dbContext.Dishes.Where(d => d.DishId == dishId).Select(d => new DishRecordDTO
            {
                DishId = d.DishId,
                DishImagePath = d.DishImagePath,
                DishName = d.DishName,
                Description = d.Description,
                Steps = d.Steps,
                CreationDateTime = d.CreationDateTime,
                IsDeleted = d.IsDeleted
            }).FirstOrDefaultAsync();

            // Return the dish records if available, otherwise return an empty
            return dishRecords;
        }

        public async Task<List<DishRecordDTO>> GetAllDishRecord()
        {
            var dishRecords = await _dbContext.Dishes
                .Select(d => new DishRecordDTO
                {

                    DishId = d.DishId,
                    DishImagePath = d.DishImagePath,
                    DishName = d.DishName,
                    Description = d.Description,
                    Steps = d.Steps,
                    CreationDateTime = d.CreationDateTime,
                    IsDeleted = d.IsDeleted

                })
                .ToListAsync();

            // Return the list of dish records if available, otherwise return an empty list
            return dishRecords;


        }

        #endregion Read

        #region Update
        public async Task<string> UpdateDish(CreateOrUpdateDishDTO dto, bool isAdmin = false)
        {
            var dish = await _dbContext.Dishes.FirstOrDefaultAsync(d => d.DishId == dto.DishId);


            if (dish == null)
            {
                return "Dish not found";
            }


            dish.DishImagePath = dto.DishImagePath;
            dish.DishName = dto.DishName;
            dish.Description = dto.Description;
            dish.Steps = dto.Steps;


            try
            {
                _dbContext.Update(dish);
                await _dbContext.SaveChangesAsync();
                return "Dish updated successfully";
            }
            catch (Exception ex)
            {
                return ex.Message + "\n" + ex.StackTrace;
            }


        }

        #endregion Update

        #region Delete

        public async Task<int> DeleteDish(int dishId, bool isAdmin = false)
        {
            if (await _dbContext.Dishes.AnyAsync(d => d.DishId == dishId))
            {
                var dish = await _dbContext.Dishes.FindAsync(dishId);

                _dbContext.Remove(dish);
                await _dbContext.SaveChangesAsync();

                return 200; // Ok
            }
            else
            {
                return 404; // Not found
            }
        }

        #endregion Delete

        #endregion Dish Management

        #region User Account Management

        #region Create
        public async Task<string> CreateDonationType(CreateOrUpdateDonationTypeDTO dto)
        {
            try
            {
                Donation donation = new Donation();
                donation.Point = dto.Point;
                donation.CardType = dto.CardType;
                donation.Price = dto.Price;
                donation.PaymentMethod = dto.PaymentMethod;

                await _dbContext.AddAsync(donation);
                await _dbContext.SaveChangesAsync();

                // donation type created successfully
                return "200";
            }
            catch (Exception ex)
            {
                // error occurred while creating the donation type
                return ex.Message + "\n" + ex.StackTrace;
            }

        }
        #endregion Create

        #region Login

        public async Task<int> Login(LoginRequestDTO dto)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Username == dto.Username && c.Password == dto.Password);
            if (client != null)
            {
                return 200; //Ok
            }
            else
            {
                return 404; // Not found
            }
        }

        #endregion Login

        #region Logout

        public async Task<int> Logout(int id)
        {
            var client = await _dbContext.Clients.FindAsync(id);
            var admin = await _dbContext.Admins.FindAsync(id);

            if (client != null || admin != null)
            {
                await _dbContext.SaveChangesAsync();
                return 200; //Ok
            }
            else
            {
                return 500; // Internal Server Error
            }
        }


        #endregion Logout

        #region ResetPassword
        public async Task<int> ResetPassword(ResetPasswordDTO dto)
        {
            if (dto.NewPassword != dto.ConfirmNewPassword)
            {
                return 400; // Bad request due to password mismatch
            }

            var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Username == dto.Username);
            if (client != null)
            {
                client.Password = dto.NewPassword;

                await _dbContext.SaveChangesAsync();

                return 200; // OK
            }
            else
            {
                return 404; // Not found
            }
        }
        #endregion ResetPassword

        #region Delete
        public async Task<int> DeleteAccount(int clientId)
        {
            if (await _dbContext.Clients.AnyAsync(c => c.ClientId == clientId))
            {
                var client = await _dbContext.Clients.FindAsync(clientId);

                _dbContext.Remove(client);
                await _dbContext.SaveChangesAsync();
                return 200; // Ok
            }
            else
            {
                return 404; // Not found
            }

        }
        #endregion Delete

        #endregion User Account Management

        #region Review Management

        #region Read
        public async Task<List<ReviewRecordDTO>> GetAllReviewRecord()
        {
            var reviewRecords = await _dbContext.Reviews.Select(r => new ReviewRecordDTO
            {
                ReviewId = r.ReviewId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreationDateTime = r.CreationDateTime,
                IsDeleted = r.IsDeleted

            }).ToListAsync();

            // Return the list of review records if available, otherwise return an empty list
            return reviewRecords;
        }
        #endregion Read

        #region Delete
        public async Task<int> DeleteReview(int reviewId, bool isAdmin = false)
        {
            if (await _dbContext.Reviews.AnyAsync(r => r.ReviewId == reviewId))
            {
                var review = await _dbContext.Reviews.FindAsync(reviewId);

                _dbContext.Remove(review);
                await _dbContext.SaveChangesAsync();

                return 200; // Ok
            }
            else
            {
                return 404; // Not found
            }

        }
        #endregion Delete

        #endregion Review Management

        #region Donation Management

        #region Read
        public async Task<List<DonationRecordDTO>> GetAllDonationRecord()
        {
            var donationRecords = await _dbContext.Donations.Select(d => new DonationRecordDTO
            {
                DonationId = d.DonationId,
                PaymentMethod = d.PaymentMethod,
                Point = d.Point,
                CardType = d.CardType,
                Price = d.Price,
                CreationDateTime = d.CreationDateTime,
                IsDeleted = d.IsDeleted

            }).ToListAsync();

            // Return the list of donation records if available, otherwise return an empty list
            return donationRecords;
        }
        #endregion Read

        #endregion Donation Management


    }
}
