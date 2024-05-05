using FoodRecipes_Core.Dtos.Account;
using FoodRecipes_Core.Dtos.Client;
using FoodRecipes_Core.Dtos.Dish;
using FoodRecipes_Core.Dtos.Donation;
using FoodRecipes_Core.Dtos.Login;
using FoodRecipes_Core.Dtos.Review;
using FoodRecipes_Core.IRepositories;
using FoodRecipes_Core.IServices;
using FoodRecipes_Core.Models.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FoodRecipes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharedController : ControllerBase
    {

        private readonly ISharedServiceInterface _sharedService;
        private readonly FoodRecipesDbContext _foodRecipesDbContext;
        public SharedController(ISharedServiceInterface sharedService, FoodRecipesDbContext foodRecipesDbContex)
        {
            _sharedService = sharedService;
            _foodRecipesDbContext = foodRecipesDbContex;
        }


        #region Donation Management Endpoints

        #region Post
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateDonationType([FromHeader] string ApiKey, [FromBody] CreateOrUpdateDonationTypeDTO dto)
        {
            try
            {
                var userKey = await _foodRecipesDbContext.Logins.FirstOrDefaultAsync(l => l.ApiKey == ApiKey);
                if (userKey == null )
                {
                    return Unauthorized("Invalid API Key");
                }
                else
                {
                    var createDonation = await _sharedService.CreateDonationType(dto);
                    if (createDonation.Contains("successfully"))
                    {
                        return Ok(createDonation);
                    }
                    else
                    {
                        return BadRequest(createDonation);
                    }

                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            #endregion Post

            #region Get


            [HttpGet]
            [Route("[action]")]
            async Task<IActionResult> GetAllDonationRecord([FromHeader] string ApiKey)
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
                        var donationRecord = await _sharedService.GetAllDonationRecord();
                        if (donationRecord != null)
                        {
                            return Ok(donationRecord);
                        }
                        else
                        {
                            return NotFound("No any donation record");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        }
        #endregion Get

        #endregion Donation Management Endpoints

        #region Dish Management Endpoints

        #region Post
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateDish([FromHeader] string ApiKey, [FromBody] CreateOrUpdateDishDTO dto)
        {
            try
            {
                var userKey = await _foodRecipesDbContext.Logins.FirstOrDefaultAsync(l => l.ApiKey == ApiKey);
                if (userKey == null )
                {
                    return Unauthorized("Invalid API Key");
                }
                else
                {
                    var dish = await _sharedService.CreateDish(dto);
                    if (dish.Contains("successfully"))
                    {
                        return Ok(dish);
                    }
                    else if (dish.Contains("Failed"))
                    {
                        return BadRequest(dish);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion Post

        #region Get
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetDishRecordByName([FromHeader] string ApiKey, [FromQuery] string dishName)
        {
            try
            {
                var userKey = await _foodRecipesDbContext.Logins.FirstOrDefaultAsync(l => l.ApiKey == ApiKey);
                if (userKey == null )
                {
                    return Unauthorized("Invalid API Key");
                }
                else
                {
                    var dish = await _sharedService.GetDishRecordByName(dishName);
                    if (dish != null)
                    {
                        return Ok(dish);
                    }
                    else
                    {
                        return NotFound($"No any dish with Name: {dishName}");
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetDishRecordById([FromHeader] string ApiKey,[FromRoute] int dishId)
        {
            try
            {
                var userKey = await _foodRecipesDbContext.Logins.FirstOrDefaultAsync(l => l.ApiKey == ApiKey);
                if (userKey == null )
                {
                    return Unauthorized("Invalid API Key");
                }
                else
                {
                    var dish = await _sharedService.GetDishRecordById(dishId);
                    if (dish != null)
                    {
                        return Ok(dish);
                    }
                    else
                    {
                        return NotFound($"No any dish with ID: {dishId}");
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllDishRecord([FromHeader] string ApiKey)
        {
            try
            {
                var userKey = await _foodRecipesDbContext.Logins.FirstOrDefaultAsync(l => l.ApiKey == ApiKey);
                if (userKey == null )
                {
                    return Unauthorized("Invalid API Key");
                }
                else
                {
                    var donation = await _sharedService.GetAllDonationRecord();
                    if (donation != null)
                    {
                        return Ok(donation);
                    }
                    else
                    {
                        return NotFound("No any dish record");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion Get



        #region Put
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateDish([FromHeader] string ApiKey, [FromQuery] bool isAdmin, [FromBody] CreateOrUpdateDishDTO dto)
        {
            try
            {
                var userKey = await _foodRecipesDbContext.Logins.FirstOrDefaultAsync(l => l.ApiKey == ApiKey);
                if (userKey == null )
                {
                    return Unauthorized("Invalid API Key");
                }
                else
                {
                    var dish = await _sharedService.UpdateDish(dto);
                    if (dish.Contains("not found"))
                    {
                        return NotFound(dish);
                    }
                    else if (dish.Contains("successfully"))
                    {
                        return Ok(dish);
                    }
                    else
                    {
                        return BadRequest(dish);
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
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteDish([FromHeader] string ApiKey, [FromRoute] int dishId)
        {
            try
            {
                var userKey = await _foodRecipesDbContext.Logins.FirstOrDefaultAsync(l => l.ApiKey == ApiKey);
                if (userKey == null )
                {
                    return Unauthorized("Invalid API Key");
                }
                else
                {
                    var dish = await _sharedService.DeleteDish(dishId);
                    if (dish.Contains("successfully"))
                    {
                        return Ok(dish);
                    }
                    else
                    {
                        return NotFound(dish);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion Delete
        #endregion Dish Management Endpoints

        #region Account Management Endpoints

        #region Login

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromHeader] string ApiKey, [FromBody] LoginRequestDTO dto)
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
                    var login = await _sharedService.Login(dto);
                    if (login.Contains("successful"))
                    {
                        return Ok(login);
                    }
                    else if (login.Contains("not found"))
                    {
                        return NotFound(login);
                    }
                    else
                    {
                        return BadRequest(login);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion Login

        #region Logout

        [HttpPost]
        [Route("[action]")]

        public async Task<IActionResult> Logout([FromHeader] string ApiKey, [FromQuery] int id)
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
                    var logout = await _sharedService.Logout(id);
                    return logout.Contains("successful") ? Ok(logout) : NotFound(logout);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion Logout

        #region ResetPassword

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ResetPassword([FromHeader] string ApiKey, [FromBody] ResetPasswordDTO dto)
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
                    var resetPassword = await _sharedService.ResetPassword(dto);
                    if (resetPassword.Contains("successfully"))
                    {
                        return Ok(resetPassword);
                    }
                    else if (resetPassword.Contains("mismatch"))
                    {
                        return BadRequest(resetPassword);
                    }
                    else
                    {
                        return BadRequest(resetPassword);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion ResetPassword

        #region Delete


        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteAccount([FromHeader] string ApiKey, [FromRoute] int clientId)
        {
            try
            {
                var userKey = await _foodRecipesDbContext.Logins.FirstOrDefaultAsync(l => l.ApiKey == ApiKey);
                
                if (userKey == null )
                {
                    return Unauthorized("Invalid API Key");
                }
                else
                {
                    var account = await _sharedService.DeleteAccount(clientId);
                    return account.Contains("successfully") ? Ok(account) : NotFound(account);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion Delete
        #endregion Account Management Endpoints

        #region Review Management Endpoints

        #region Get

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllReviewRecord([FromHeader] string ApiKey)
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
                    var review = await _sharedService.GetAllReviewRecord();
                    if (review != null)
                    {
                        return Ok(review);
                    }
                    else
                    {
                        return NotFound(review);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion Get

        #region Delete

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteReview([FromHeader] string ApiKey, [FromRoute] int reviewId)
        {
            try
            {
                var userKey = await _foodRecipesDbContext.Logins.FirstOrDefaultAsync(l => l.ApiKey == ApiKey);
                if (userKey == null )
                {
                    return Unauthorized("Invalid API Key");
                }
                else
                {
                    var review = await _sharedService.DeleteReview(reviewId);
                    if (review.Contains("successfully"))
                    {
                        return Ok(review);
                    }
                    else
                    {
                        return NotFound(review);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion Delete

        #endregion Review Management Endpoints






    }
}
