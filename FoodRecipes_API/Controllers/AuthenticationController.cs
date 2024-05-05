using FoodRecipes_Core.Dtos.Login;
using FoodRecipes_Core.Models.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodRecipes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly FoodRecipesDbContext _foodRecipesDbContext;
        private readonly IConfiguration _configuration;

        public AuthenticationController(FoodRecipesDbContext foodRecipesDbContext, IConfiguration configuration)
        {
            _foodRecipesDbContext = foodRecipesDbContext;
            _configuration = configuration;
        }

        [NonAction]
        public string GetRandomString(int length = 20)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            byte[] data = new byte[length];

            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(data);
            }

            StringBuilder result = new StringBuilder(length);
            foreach (byte b in data)
            {
                result.Append(chars[b % chars.Length]);
            }

            return result.ToString();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> LoginToGetApiKey(LoginDTO dto)
        {
            try
            {
                var user = await _foodRecipesDbContext.Logins.FirstOrDefaultAsync(x => x.Username == dto.Username && x.Password == dto.Password);
                if (user == null)
                {
                    return Unauthorized("Username or Password is incorrect");
                }

                // Generate a unique API key
                string apikey = GetRandomString();
                user.ApiKey = apikey;

                _foodRecipesDbContext.Update(user);
                await _foodRecipesDbContext.SaveChangesAsync();

                return Ok(new { message = "Login successful", apikey });
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("[action]")]
        //public async Task<IActionResult> ClientLoginToGetApiKey(LoginDTO dto)
        //{
        //    try
        //    {
        //        var user = await _foodRecipesDbContext.Clients.FirstOrDefaultAsync(x => x.Username == dto.Username && x.Password == dto.Password);
        //        if (user == null)
        //        {
        //            return Unauthorized("Username or Password is incorrect");
        //        }

        //        // Generate a unique API key
        //        string apikey = GetRandomString();
        //        user.ApiKey = apikey;

        //        _foodRecipesDbContext.Update(user);
        //        await _foodRecipesDbContext.SaveChangesAsync();

        //        return Ok(new { message = "Login successful", apikey });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"An error occurred: {ex.Message}");
        //    }
        //}

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GenerateJWTTokenForClient(LoginRequestDTO dto)
        {
            var user = await _foodRecipesDbContext.Logins.FirstOrDefaultAsync
                  (x => x.Username == dto.Username && x.Password == dto.Password);
            if (user == null)
            {
                return Unauthorized("Username or Password is uncorrect");
            }
            else
            {
                var profile = await _foodRecipesDbContext.Clients.FirstOrDefaultAsync
                    (x => x.Email == dto.Username);
                //Generate Token
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes("FoodRecipesAPISecrectJWTJoken2024Web10");
                var tokenDescriptior = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("Username",profile.ClientId.ToString()),
                        new Claim("UserType","Paitent")
                    }),
                    Expires = DateTime.Now.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey)
                    , SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptior);
                return Ok(tokenHandler.WriteToken(token));
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GenerateJWTTokenForAdmin(LoginRequestDTO dto)
        {
            var user = await _foodRecipesDbContext.Logins.FirstOrDefaultAsync
                   (x => x.Username == dto.Username && x.Password == dto.Password);
            if (user == null)
            {
                return Unauthorized("Username or Password is uncorrect");
            }
            else
            {
                var profile = await _foodRecipesDbContext.Admins.FirstOrDefaultAsync
                    (x => x.Username == dto.Username);
                //Generate Token
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes("FoodRecipesAPISecrectJWTJoken2024Web10");
                var tokenDescriptior = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("Username",profile.AdminId.ToString()),
                        new Claim("UserType","Admin")
                    }),
                    Expires = DateTime.Now.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey)
                    , SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptior);
                return Ok(tokenHandler.WriteToken(token));
            }

        }
    }
}