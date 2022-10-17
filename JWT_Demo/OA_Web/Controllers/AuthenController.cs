using Microsoft.AspNetCore.Mvc;
using OA_Data;
using OA_Service;
using OA_Web.Models;
using System.Security.Claims;

namespace OA_Web.Controllers
{
    public class AuthenController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthenController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }
    }
}
