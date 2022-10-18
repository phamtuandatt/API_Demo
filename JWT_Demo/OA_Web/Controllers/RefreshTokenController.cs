using Microsoft.AspNetCore.Mvc;
using OA_Repo;
using OA_Service;
using OA_Web.Models;

namespace OA_Web.Controllers
{
    public class RefreshTokenController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public RefreshTokenController(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult Refresh()
        {
            return View();
        }

        [HttpPost]
        // [FromBody] to run with PostMan
        public  IActionResult Refresh([FromBody] TokenAPI tokenApiModel)
        {
            if (tokenApiModel is null)
            {
                return NotFound("Invalid client request");
            }

            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default
            var user = _context.LoginModels.SingleOrDefault(u => u.UserName == username);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime >= DateTime.Now)
            {
                return BadRequest("Invalid client request"); 
            }
            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            _context.SaveChanges();
            return Ok(new AuthenticateRespone()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        public IActionResult Revoke()
        {
            var username = User.Identity.Name;
            var user = _context.LoginModels.SingleOrDefault(u => u.UserName == username);
            if (user == null)
            {
                return BadRequest();
            }
            user.RefreshToken = null;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
