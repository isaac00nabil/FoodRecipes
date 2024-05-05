using FoodRecipes_Core.Dtos.Client;
using FoodRecipes_Core.Dtos.Donation;
using FoodRecipes_Core.Dtos.Review;
using FoodRecipes_Core.IRepositories;
using FoodRecipes_Core.IServices;
using FoodRecipes_Core.Models.Context;
using FoodRecipes_Core.Models.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodRecipes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientServiceInterface _clientServiceInterface;
        private readonly FoodRecipesDbContext _foodRecipesDbContext;

        public ClientController(IClientServiceInterface clientServiceInterface, FoodRecipesDbContext foodRecipesDbContext)
        {
            _clientServiceInterface = clientServiceInterface;
            _foodRecipesDbContext = foodRecipesDbContext;
        }

        #region Client Management Endpoints

        #region Post
        [HttpPost]
        [Route("[action]")]

        public async Task<IActionResult> CreateAccount([FromBody] ClientRegistrationDTO dto)
        {
            try
            {
                var createAccount = await _clientServiceInterface.CreateAccount(dto);
                if (createAccount.Contains("successfully"))
                {
                    return Ok(createAccount);
                }
                else
                {
                    return BadRequest(createAccount);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion Post

        #region Get

        #endregion Get

        #region Put
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateAccount([FromHeader] string ApiKey, [FromBody] ClientUpdateProfileDTO dto)
        {
            try
            {
                var userKey = await _foodRecipesDbContext.Logins.FirstOrDefaultAsync(l => l.ApiKey == ApiKey);

                if (userKey == null)
                {
                    return Unauthorized("Invalid API Key");
                }
                else
                {
                    {
                        var account = await _clientServiceInterface.UpdateAccount(dto);
                        if (account.Contains("not found"))
                        {
                            return NotFound(account);
                        }
                        else if (account.Contains("successfully"))
                        {
                            return Ok(account);
                        }
                        else
                        {
                            return BadRequest(account);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion Put

        #region Delete

        #endregion Delete
        #endregion Client Management Endpoints

        #region Donation Management Endpoints

        #region Get
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetDonationRecordByClientId([FromHeader] string ApiKey, [FromRoute] int clientId)
        {
            try
            {
                var userKey = await _foodRecipesDbContext.Logins.FirstOrDefaultAsync(l => l.ApiKey == ApiKey);

                if (userKey == null)
                {
                    return Unauthorized("Invalid API Key");
                }
                else
                {
                    var donationList = await _clientServiceInterface.GetDonationRecordByClientId(clientId);
                    if (donationList != null)
                    {
                        return Ok(donationList);
                    }
                    else
                    {
                        return NotFound($"no any donation for Client ID: {clientId}");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion Donation Management Endpoints

        #endregion Donation Management Endpoints

        #region Review Management Endpoints

        #region Post
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateNewReview([FromHeader] string ApiKey, [FromBody] CreateOrUpdateReviewDTO dto)
        {
            try
            {
                var userKey = await _foodRecipesDbContext.Logins.FirstOrDefaultAsync(l => l.ApiKey == ApiKey);

                if (userKey == null)
                {
                    return Unauthorized("Invalid API Key");
                }
                else
                {
                    var createReview = await _clientServiceInterface.CreateNewReview(dto);
                    if (createReview.Contains("successfully"))
                    {
                        return Ok(createReview);
                    }
                    else
                    {
                        return BadRequest(createReview);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion Post

        #region Put
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateReview([FromHeader] string ApiKey, [FromBody] CreateOrUpdateReviewDTO dto)
        {
            try
            {
                var userKey = await _foodRecipesDbContext.Logins.FirstOrDefaultAsync(l => l.ApiKey == ApiKey);

                if (userKey == null)
                {
                    return Unauthorized("Invalid API Key");
                }
                else
                {
                    var review = await _clientServiceInterface.UpdateReview(dto);
                    if (review.Contains("not found"))
                    {
                        return NotFound(review);
                    }
                    else if (review.Contains("successfully"))
                    {
                        return Ok(review);
                    }
                    else
                    {
                        return BadRequest(review);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion Put

        #endregion Review Management Endpoints
    }
}
