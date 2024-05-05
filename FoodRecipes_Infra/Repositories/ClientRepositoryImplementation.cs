using FoodRecipes_Core.Dtos.Client;
using FoodRecipes_Core.Dtos.Donation;
using FoodRecipes_Core.Dtos.Review;
using FoodRecipes_Core.IRepositories;
using FoodRecipes_Core.Models.Context;
using FoodRecipes_Core.Models.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
    public class ClientRepositoryImplementation : IClientRepositoryInterface
    {
        private readonly FoodRecipesDbContext _context;
        public ClientRepositoryImplementation(FoodRecipesDbContext context)
        {
            _context = context;
        }

        #region  Client Management

        #region Create 

        public async Task<string> CreateAccount(ClientRegistrationDTO dto)
        {

            try
            {
                Client client = new Client();
                client.FirstName = dto.FirstName;
                client.LastName = dto.LastName;
                client.Username = dto.Username.ToLower();
                client.Email = dto.Email.ToLower();
                client.Password = dto.Password;

                await _context.AddAsync(client);
                await _context.SaveChangesAsync();

                //Login login = new Login();
                //login.Username = client.Email;
                //login.Password = client.Password;
                //login.Clients = client;
                //await _context.AddAsync(login);
                //await _context.SaveChangesAsync();
                // Client account successfully created
                return "200";
            }
            catch (Exception ex)
            {
                // Error occurred while creating the client account
                return ex.Message + "\n" + ex.StackTrace;
            }


        }

        #endregion Create

        #region Update

        public async Task<string> UpdateAccount(ClientUpdateProfileDTO dto)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(a => a.ClientId == dto.ClientId);

            if (client == null)
            {
                return "Client not found";
            }

            // Update client account properties with values from the DTO
            client.ProfileImagePath = dto.ProfileImagePath;
            client.FirstName = dto.FirstName;
            client.LastName = dto.LastName;
            client.Username = dto.Username;
            client.Email = dto.Email;
            client.Password = dto.Password;

            try
            {
                _context.Clients.Update(client);
                await _context.SaveChangesAsync();
                return "Client account updated successfully";
            }
            catch (Exception ex)
            {
                // Handle the exception and log it
                return ex.Message + "\n" + ex.StackTrace;

            }


        }
        #endregion Update

        #endregion Client Management


        #region Review Management

        #region Create
        public async Task<string> CreateNewReview(CreateOrUpdateReviewDTO dto)
        {
            try
            {
                Review review = new Review();

                review.Rating = dto.Rating;
                review.Comment = dto.Comment;
                await _context.AddAsync(review);
                await _context.SaveChangesAsync();

                // Create review successfully created
                return "200";
            }
            catch (Exception ex)
            {
                // Error occurred while creating the client account
                return ex.Message + "\n" + ex.StackTrace;
            }

        }

        #endregion Create

        #region Update

        public async Task<string> UpdateReview(CreateOrUpdateReviewDTO dto)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.ReviewId == dto.ReviewId);
            if (review == null)
            {
                return "Review not found";
            }
            // Update the review type properties with values from the DTO
            review.Rating = dto.Rating;
            review.Comment = dto.Comment;

            try
            {
                _context.Update(review);
                await _context.SaveChangesAsync();
                return "Review updated successfully";
            }
            catch (Exception ex)
            {
                // Handle the exception and log it
                return ex.Message + "\n" + ex.StackTrace;
            }



        }

        #endregion Update

        #endregion Review  Management


        #region Donation Management

        #region Read
        public async Task<List<DonationRecordDTO>> GetDonationRecordByClientId(int clientId)
        {
            var client = await _context.Clients.Include(c => c.Donations).FirstOrDefaultAsync(c => c.ClientId == clientId);
            if (client != null)
            {
                var donationRecords = client.Donations.Select(d => new DonationRecordDTO
                {
                    DonationId = d.DonationId,
                    Point = d.Point,
                    CardType = d.CardType,
                    Price = d.Price,
                    PaymentMethod = d.PaymentMethod
                }).ToList();
                return donationRecords;
            }
            else
            {
                return new List<DonationRecordDTO>(); ;
            }
        }

        #endregion Read

        #endregion Donation Management



        #region  Management

        #region Create
        #endregion Create

        #region Read
        #endregion Read

        #region Update
        #endregion Update

        #region Delete
        #endregion Delete

        #endregion  Management

    }
}
