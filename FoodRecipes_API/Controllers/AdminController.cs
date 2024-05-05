using FoodRecipes_Core.Dtos.Admin;
using FoodRecipes_Core.Dtos.Donation;
using FoodRecipes_Core.Dtos.FoodSection;
using FoodRecipes_Core.Helper.Enums;
using FoodRecipes_Core.IServices;
using FoodRecipes_Core.Models.Context;
using FoodRecipes_Core.Models.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;

namespace FoodRecipes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAdminServiceInterface _adminServiceInterface;
        private readonly FoodRecipesDbContext _foodRecipesDbContext;
        public AdminController(IAdminServiceInterface adminServiceInterface, FoodRecipesDbContext foodRecipesDbContext)
        {
            _adminServiceInterface = adminServiceInterface;
            _foodRecipesDbContext = foodRecipesDbContext;
        }


        #region Admin Management Endpoints

        #region Post

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateNewAdmin([FromBody] CreateOrUpdateAdminAccountDTO dto)
        {
            try
            {
                var createNewAdmin = await _adminServiceInterface.CreateAdminAccount(dto);
                if (createNewAdmin.Contains("successfully"))
                {
                    return Ok(createNewAdmin);
                }
                else
                {
                    return BadRequest(createNewAdmin);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        #endregion Post

        #region Delete

        [HttpDelete]
        [Route("[action]/{adminId}")]
        public async Task<IActionResult> DeleteAccount([FromHeader] string ApiKey, [FromRoute] int adminId)
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

                    var deleteAdmin = await _adminServiceInterface.DeleteAdminAccount(adminId);
                    if (deleteAdmin.Contains("successfully"))
                    {
                        return Ok(deleteAdmin);
                    }
                    else
                    {
                        return NotFound(deleteAdmin);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        #endregion Delete

        #endregion  Admin Management Endpoints


        #region Donation Management Endpoints

        #region Get

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllDonationRecord([FromHeader] string ApiKey)
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
                    var donationHistory = await _adminServiceInterface.GetAllDonationRecord();
                    if (donationHistory.IsNullOrEmpty())
                    {
                        return NotFound("No Donation Found");
                    }
                    else
                    {
                        return Ok(donationHistory);
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
        public async Task<IActionResult> GetDonationRecordByCardType([FromHeader] string ApiKey, [FromRoute]string cardType)
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
                    var donationHistory = await _adminServiceInterface.GetDonationRecordByCardType(cardType);
                    if (donationHistory != null)
                    {
                        return Ok(donationHistory);

                    }
                    else
                    {
                        return NotFound("No Donation Found");
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

        public async Task<IActionResult> GetDonationRecordById([FromHeader] string ApiKey,[FromRoute] int donationId)
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
                    var donationRecord = await _adminServiceInterface.GetDonationRecordById(donationId);
                    if (donationRecord != null)
                    {

                        return Ok(donationRecord);
                    }
                    else
                    {
                        return NotFound("No Donation Found");
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
        public async Task<IActionResult> UpdateDonationType([FromHeader] string ApiKey, [FromBody] CreateOrUpdateDonationTypeDTO dto)
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

                    var updateDonation = await _adminServiceInterface.UpdateDonationType(dto);

                    switch (updateDonation)
                    {
                        case HttpStatusCode.NotFound:
                            return NotFound("Donation type not found");
                        case HttpStatusCode.OK:
                            return Ok("Donation type updated successfully");
                        default:
                            return BadRequest("Failed to update donation type");

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
        [Route("[action]/{cardType}")]
        public async Task<IActionResult> DeleteDonationTypeByCardType([FromHeader] string ApiKey, [FromRoute] string cardType)
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
                    var donationType = await _adminServiceInterface.DeleteDonationTypeByCardType(cardType);
                    if (donationType.Contains("deleted"))
                    {
                        return Ok(donationType);
                    }
                    else
                    {
                        return NotFound(donationType);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("[action]/{donationId}")]
        public async Task<IActionResult> DeleteDonationTypeById([FromHeader] string ApiKey, [FromRoute] int donationId)
        {
            await _adminServiceInterface.DeleteDonationTypeById(donationId);
            try
            {
                var userKey = await _foodRecipesDbContext.Logins.FirstOrDefaultAsync(l => l.ApiKey == ApiKey);
                if (userKey == null)
                {
                    return Unauthorized("Invalid API Key");
                }
                else
                {
                    var donationType = await _adminServiceInterface.DeleteDonationTypeById(donationId);
                    if (donationType.Contains("deleted"))
                    {
                        return Ok(donationType);
                    }
                    else
                    {
                        return NotFound(donationType);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion Delete

        #endregion Donation Management Endpoints

        #region FoodSection Management Endpoints

        #region Post

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateNewFoodSection([FromHeader] string ApiKey, [FromBody] CreateOrUpdateFoodSectionDTO dto)
        {

            var foodSection = await _adminServiceInterface.CreateNewFoodSection(dto);
            try
            {
                var userKey = await _foodRecipesDbContext.Logins.FirstOrDefaultAsync(l => l.ApiKey == ApiKey);
                if (userKey == null)
                {
                    return Unauthorized("Invalid API Key");
                }
                else
                {
                    if (foodSection == "200")
                    {
                        return Ok(foodSection);
                    }
                    else
                    {
                        return StatusCode(500, foodSection);
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
        public async Task<IActionResult> UpdateFoodSection([FromHeader] string ApiKey, [FromBody] CreateOrUpdateFoodSectionDTO dto)
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
                    var foodSection = await _adminServiceInterface.UpdateFoodSection(dto);
                    if (foodSection.Contains("not found"))
                    {
                        return NotFound(foodSection);
                    }
                    else if (foodSection.Contains("successfully"))
                    {
                        return Ok(foodSection);
                    }
                    else
                    {
                        return BadRequest($"Failed to update FoodSection updated: {foodSection}");
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
        [Route("[action]/{foodSectionId}")]
        public async Task<IActionResult> DeleteFoodSectionById([FromHeader] string ApiKey, [FromRoute] int foodSectionId)
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
                    var foodSection = await _adminServiceInterface.DeleteFoodSectionById(foodSectionId);

                    if (foodSection.Contains("not found"))
                    {
                        return NotFound(foodSection);
                    }
                    else if (foodSection.Contains("successfully"))
                    {
                        return Ok(foodSection);
                    }
                    else
                    {
                        return BadRequest($"Failed to update FoodSection updated: {foodSection}");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        #endregion Delete

        #endregion FoodSection Management Endpoints

        #region Client Management Endpoints

        #region Get

        [HttpGet]
        [Route("[action]/users")]
        public async Task<IActionResult> GetAllClient([FromHeader] string ApiKey)
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
                    var Clients = await _adminServiceInterface.GetAllClient();

                    if (Clients != null)
                    {
                        return Ok(Clients);
                    }
                    else
                    {
                        return NotFound("No Client Found");
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
        public async Task<IActionResult> GetClientById([FromHeader] string ApiKey,[FromRoute] int clientId)
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
                    var clientById = await _adminServiceInterface.GetClientById(clientId);
                    if (clientById != null)
                    {
                        return Ok(clientById);
                    }
                    else
                    {
                        return NotFound($"Client with ID: {clientId} Not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetClientByUsername([FromHeader] string ApiKey,[FromRoute] string username)
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
                    var clientUsername = await _adminServiceInterface.GetClientByUsername(username);

                    if (clientUsername != null)
                    {
                        return Ok(clientUsername);
                    }
                    else
                    {
                        return NotFound($"Client with Username: {username} Not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        #endregion Get

        #endregion Client Management Endpoints

        #region Review Management Endpoints

        #region Get

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetReviewRecordByClientId([FromHeader] string ApiKey,[FromRoute] int clientId)
        {
            var review = await _adminServiceInterface.GetReviewRecordByClientId(clientId);
            try
            {
                var userKey = await _foodRecipesDbContext.Logins.FirstOrDefaultAsync(l => l.ApiKey == ApiKey);
                if (userKey == null)
                {
                    return Unauthorized("Invalid API Key");
                }
                else
                {
                    if (review != null)
                    {
                        return Ok(review);
                    }
                    else
                    {
                        return NotFound($"Review for Client with ID: {clientId} Not found.");
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
        public async Task<IActionResult> GetReviewRecordById([FromHeader] string ApiKey,[FromRoute] int reviewId)
        {
            var review = await _adminServiceInterface.GetReviewRecordById(reviewId);
            try
            {
                var userKey = await _foodRecipesDbContext.Logins.FirstOrDefaultAsync(l => l.ApiKey == ApiKey);
                if (userKey == null)
                {
                    return Unauthorized("Invalid API Key");
                }
                else
                {
                    if (review != null)
                    {
                        return Ok(review);
                    }
                    else
                    {
                        return NotFound($"Review with ID: {reviewId} Not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        #endregion Get

        #endregion Review Management Endpoints


    }
}
