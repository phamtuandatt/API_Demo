using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OA_Data;
using OA_Service;
using OA_Web.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OA_Web.Controllers
{
    public class UsersController : Controller
    {
        private IConfiguration _config;
        private readonly IUserService userService;

        public UsersController(IConfiguration config, IUserService userService)
        {
            _config = config;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login( User login)
        {
            var user = Authenticate(login);
            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");

        }

        private string Generate(Users user)
        {
            var sercurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(sercurityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Check login user
        private Users Authenticate(User login)
        {
            var model = userService.CheckLogin(login.UserName, login.Password);
            if (model != null)
            {
                return model;
            }

            return null;
        }
    }
}
