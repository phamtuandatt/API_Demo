using Microsoft.AspNetCore.Mvc;
using OA_Data;
using OA_Repo;
using OA_Service;
using OA_Web.Models;
using System.Security.Claims;

namespace OA_Web.Controllers
{
    public class AuthenController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly ApplicationDbContext _context;

        public AuthenController(ITokenService tokenService, ApplicationDbContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (loginModel is null)
            {
                return BadRequest("Invalid client request");
            }

            // Get user (username, password)
            var user = _context.LoginModels.FirstOrDefault(u =>
                (u.UserName == loginModel.UserName) && (u.Password == loginModel.Password));


            if (user is null)
            {
                return NotFound("User not found");
            }

            // Generate Token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginModel.UserName), 
                new Claim(ClaimTypes.Role, "Manager")
            };
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(20);
            
            _context.SaveChanges();
            return Ok(new AuthenticateRespone
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }
    }
}
