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
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private ApplicationDbContext _context;

        public AuthenController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
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
                return Unauthorized();
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
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            
            _context.SaveChanges();
            return Ok(new AuthenticateRespone
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }
    }
}
