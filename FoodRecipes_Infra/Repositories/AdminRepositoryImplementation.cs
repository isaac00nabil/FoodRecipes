using FoodRecipes_Core.Dtos.Admin;
using FoodRecipes_Core.Dtos.Client;
using FoodRecipes_Core.Dtos.Donation;
using FoodRecipes_Core.Dtos.FoodSection;
using FoodRecipes_Core.Dtos.Review;
using FoodRecipes_Core.Helper.Enums;
using FoodRecipes_Core.IRepositories;
using FoodRecipes_Core.Models.Context;
using FoodRecipes_Core.Models.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipes_Infra.Repositories
{
    public class AdminRepositoryImplementation : IAdminRepositoryInterface
    {

        private readonly FoodRecipesDbContext _foodRecipesDbContext;
        public AdminRepositoryImplementation(FoodRecipesDbContext foodRecipesDbContext)
        {
            _foodRecipesDbContext = foodRecipesDbContext;
        }

        #region Admin Management

        #region Create
        public async Task<string> CreateAdminAccount(CreateOrUpdateAdminAccountDTO dto)
        {
            try
            {
                Admin admin = new Admin();
                admin.Username = dto.Username.ToLower();
                admin.DisplayName = dto.DisplayName;
                admin.Password = dto.Password;
                await _foodRecipesDbContext.AddAsync(admin);
                await _foodRecipesDbContext.SaveChangesAsync();

                //Login login = new Login();
                //login.Username = admin.Username;
                //login.Password = admin.Password;
                //login.Admins = admin;
                //await _foodRecipesDbContext.AddAsync(login);
                //await _foodRecipesDbContext.SaveChangesAsync();


                // Admin account successfully created
                return ("200");

            }
            catch (Exception ex)
            {
                // Error occurred while creating the admin account
                return ex.Message + "\n" + ex.StackTrace;
            }
        }

        #endregion

        #region Delete
        public async Task<int> DeleteAdminAccount(int adminId)
        {


            var admin = await _foodRecipesDbContext.Admins.FirstOrDefaultAsync(a => a.AdminId == adminId);
            if (admin != null)
            {
                _foodRecipesDbContext.Remove(admin);
                await _foodRecipesDbContext.SaveChangesAsync();

                // Admin successfully deleted
                return 200;
            }


            // Admin not found
            return 404;

        }

        #endregion

        #endregion Admin Management

        #region Donation Management

        #region Read

        public async Task<List<DonationRecordDTO>> GetAllDonationRecord()
        {

            var donationRecords = await _foodRecipesDbContext.Donations.ToListAsync();
            if (donationRecords?.Count != 0)
            {
                var donationRecordDTOs = donationRecords.Select(d => new DonationRecordDTO
                {
                    DonationId = d.DonationId,
                    Point = d.Point,
                    CardType = d.CardType,
                    Price = d.Price,
                    PaymentMethod = d.PaymentMethod,
                    CreationDateTime = d.CreationDateTime,
                    IsDeleted = d.IsDeleted,
                }).ToList();

                // return donations list
                return donationRecordDTOs;

            }
            else
            {
                // return an empty list if there are no donations
                return new List<DonationRecordDTO>();
            }

        }

        public async Task<List<DonationRecordDTO>> GetDonationRecordByCardType(string cardType)
        {

            var donationRecords = await _foodRecipesDbContext.Donations.Where(d => d.CardType == cardType).ToListAsync();
            if (donationRecords?.Any() != false)
            {
                var donationRecordDTOs = donationRecords.Select(d => new DonationRecordDTO
                {
                    DonationId = d.DonationId,
                    Point = d.Point,
                    CardType = d.CardType,
                    Price = d.Price,
                    PaymentMethod = d.PaymentMethod,
                    CreationDateTime = d.CreationDateTime,
                    IsDeleted = d.IsDeleted,
                }).ToList();
                // return donations list
                return donationRecordDTOs;
            }
            else
            {
                // return null if there are no donations
                return null;
            }



        }

        public async Task<DonationRecordDTO> GetDonationRecordById(int donationId)
        {
            var donation = await _foodRecipesDbContext.Donations.FirstOrDefaultAsync(d => d.DonationId == donationId);
            if (donation != null)
            {
                var donationRecordDTO = new DonationRecordDTO
                {
                    DonationId = donation.DonationId,
                    Point = donation.Point,
                    CardType = donation.CardType,
                    Price = donation.Price,
                    PaymentMethod = donation.PaymentMethod,
                    CreationDateTime = donation.CreationDateTime,
                    IsDeleted = donation.IsDeleted,
                };
                // return donations list
                return donationRecordDTO;
            }
            else
            {
                // return null if there are no donations
                return null;
            }

        }


        #endregion Read

        #region Update
        public async Task<int> UpdateDonationType(CreateOrUpdateDonationTypeDTO dto)
        {
            var donationType = await _foodRecipesDbContext.Donations.FirstOrDefaultAsync(d => d.DonationId == dto.DonationId);
            if (donationType == null)
            {
                return 404;
            }

            // Update the donation type properties with values from the DTO
            donationType.CardType = dto.CardType;
            donationType.Point = dto.Point;
            donationType.Price = dto.Price;
            donationType.PaymentMethod = dto.PaymentMethod;

            try
            {
                _foodRecipesDbContext.Update(donationType);
                await _foodRecipesDbContext.SaveChangesAsync();
                return 200;
            }
            catch
            {
                // Handle the exception
                return -1;
            }

        }
        #endregion Update

        #region Delete

        public async Task<HttpStatusCode> DeleteDonationTypeByCardType(string cardType)
        {
            var donation = await _foodRecipesDbContext.Donations.FirstOrDefaultAsync(d => d.CardType == cardType);

            if (donation != null)
            {
                _foodRecipesDbContext.Remove(donation);
                await _foodRecipesDbContext.SaveChangesAsync();

                //returns 200 (OK)
                return HttpStatusCode.OK;
            }
            else
            {
                //returns 404 (Not Found).
                return HttpStatusCode.NotFound;
            }


        }

        public async Task<HttpStatusCode> DeleteDonationTypeById(int donationId)
        {
            var donation = await _foodRecipesDbContext.Donations.FindAsync(donationId);

            if (donation != null)
            {
                _foodRecipesDbContext.Remove(donation);
                await _foodRecipesDbContext.SaveChangesAsync();

                // return 200 if the deletion is successful,
                return HttpStatusCode.OK;
            }
            else
            {
                // If the donation type is not found, return a 404 status code.
                return HttpStatusCode.NotFound;
            }


        }

        #endregion Delete 

        #endregion Donation Management

        #region Food Section Management

        #region Create
        public async Task<string> CreateNewFoodSection(CreateOrUpdateFoodSectionDTO dto)
        {
            try
            {
                FoodSection foodSection = new FoodSection();

                foodSection.SectionName = dto.SectionName;
                foodSection.Description = dto.Description;

                await _foodRecipesDbContext.AddAsync(foodSection);
                await _foodRecipesDbContext.SaveChangesAsync();

                // Create a new food section. If successful, return "200" as a success code.
                return "200";

            }
            catch (Exception ex)
            {
                // If the operation fails, return the error message.
                return ex.Message + "\n" + ex.StackTrace;
            }


        }
        #endregion Create

        #region Update
        public async Task<HttpStatusCode> UpdateFoodSection(CreateOrUpdateFoodSectionDTO dto)
        {
            var foodSection = await _foodRecipesDbContext.FoodSections.FirstOrDefaultAsync(f => f.FoodSectionId == dto.FoodSectionId);

            if (foodSection == null)
            {
                return HttpStatusCode.NotFound;
            }

            // Update FoodSection properties with values from the DTO
            foodSection.SectionName = dto.SectionName;
            foodSection.Description = dto.Description;

            try
            {
                _foodRecipesDbContext.Update(foodSection);
                await _foodRecipesDbContext.SaveChangesAsync();
                return HttpStatusCode.OK;
            }
            catch
            {
                // Handle the exception and log it
                return HttpStatusCode.ExpectationFailed;
            }

        }
        #endregion Update

        #region Delete
        public async Task<int> DeleteFoodSectionById(int foodSectionId)
        {
            var foodSection = await _foodRecipesDbContext.FoodSections.FindAsync(foodSectionId);
            if (foodSection != null)
            {
                _foodRecipesDbContext.Remove(foodSection);
                await _foodRecipesDbContext.SaveChangesAsync();

                // return 200 if the deletion is successful, 
                return 200;
            }
            else
            {
                // return 404 if the food section with the specified ID is not found.
                return 404;
            }

        }
        #endregion Delete

        #endregion Food Section Management

        #region Client Management

        #region Read

        public async Task<List<ClientRecordDTO>> GetAllClient()
        {
            var clientRecord = await _foodRecipesDbContext.Clients.ToListAsync();
            if (clientRecord?.Count > 0)
            {
                var clientRecordDTOs = clientRecord.Select(c => new ClientRecordDTO
                {
                    ClientId = c.ClientId,
                    ProfileImagePath = c.ProfileImagePath,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Username = c.Username,
                    Email = c.Email,
                    UserType = c.UserType,
                    CreationDateTime = c.CreationDateTime,
                    IsDeleted = c.IsDeleted,
                }).ToList();

                // return clients list
                return clientRecordDTOs;
            }
            else
            {
                // return an empty list if there are no clients
                return new List<ClientRecordDTO>();
            }

        }

        public async Task<ClientRecordDTO> GetClientById(int clientId)
        {
            var clientRecord = await _foodRecipesDbContext.Clients.FirstOrDefaultAsync(c => c.ClientId == clientId);
            if (clientRecord != null)
            {
                var clientRecordDTO = new ClientRecordDTO
                {
                    ClientId = clientRecord.ClientId,
                    ProfileImagePath = clientRecord.ProfileImagePath,
                    FirstName = clientRecord.FirstName,
                    LastName = clientRecord.LastName,
                    Username = clientRecord.Username,
                    Email = clientRecord.Email,
                    UserType = clientRecord.UserType,
                    CreationDateTime = clientRecord.CreationDateTime,
                    IsDeleted = clientRecord.IsDeleted,
                };
                // return representing the client record
                return clientRecordDTO;
            }
            else
            {
                //return null if the client does not exist
                return null;
            }

        }

        public async Task<ClientRecordDTO> GetClientByUsername(string username)
        {

            var clientRecord = await _foodRecipesDbContext.Clients.FirstOrDefaultAsync(c => c.Username == username);
            if (clientRecord != null)
            {
                var clientRecordDTO = new ClientRecordDTO
                {
                    ClientId = clientRecord.ClientId,
                    ProfileImagePath = clientRecord.ProfileImagePath,
                    FirstName = clientRecord.FirstName,
                    LastName = clientRecord.LastName,
                    Username = clientRecord.Username,
                    Email = clientRecord.Email,
                    UserType = clientRecord.UserType,
                    CreationDateTime = clientRecord.CreationDateTime,
                    IsDeleted = clientRecord.IsDeleted,
                };
                // return representing the client record
                return clientRecordDTO;
            }
            else
            {
                //return null if the client does not exist
                return null;
            }

        }
        #endregion Read

        #endregion Client Management

        #region Review Management

        #region Read
        public async Task<List<ReviewRecordDTO>> GetReviewRecordByClientId(int clientId)
        {
            var reviewRecords = await _foodRecipesDbContext.Reviews
                .Where(r => r.ClientId == clientId)
                .ToListAsync();
            if (reviewRecords?.Count > 0)
            {
                var reviewRecordDTOs = reviewRecords.Select(r => new ReviewRecordDTO
                {
                    ReviewId = r.ReviewId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreationDateTime = r.CreationDateTime,
                    IsDeleted = r.IsDeleted

                }).ToList();
                // return records list
                return reviewRecordDTOs;
            }
            else
            {
                // return an empty list if there are no records
                return new List<ReviewRecordDTO>();
            }

        }

        public async Task<ReviewRecordDTO> GetReviewRecordById(int reviewId)
        {

            var review = await _foodRecipesDbContext.Reviews.FirstOrDefaultAsync(r => r.ReviewId == reviewId);
            if (review != null)
            {
                var reviewRecordDTO = new ReviewRecordDTO
                {
                    ReviewId = review.ReviewId,
                    Rating = review.Rating,
                    Comment = review.Comment,
                    CreationDateTime = review.CreationDateTime,
                    IsDeleted = review.IsDeleted
                };
                // return representing the review record
                return reviewRecordDTO;
            }
            else
            {
                //return null if the review does not exist
                return null;
            }

        }


        #endregion Read

        #endregion Review Management

    }
}
