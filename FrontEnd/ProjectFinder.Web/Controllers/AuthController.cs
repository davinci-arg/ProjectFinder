using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProjectFinder.Web.Models;
using ProjectFinder.Web.Service.IService;
using ProjectFinder.Web.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProjectFinder.Web.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly ITokenProvider _tokenProvider;

    public AuthController(IAuthService authService, ITokenProvider tokenProvider)
    {
        _authService = authService;
        _tokenProvider = tokenProvider;
    }

    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        LoginRequestDto loginRequestDto = new();
        return View(loginRequestDto);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDto model)
    {
        ResponseDto result = await _authService.LoginAsync(model);

        if (result.IsSuccess && result != null)
        {
            LoginResponseDto loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(result.Result));

            await SingInUser(loginResponse);
            _tokenProvider.SetToken(loginResponse.Token);
            TempData["success"] = "Login seccessful";

            return RedirectToAction("Finder", "GitHubFinder");
        }
        else
        {
            TempData["error"] = result?.Message ?? "An error occurred during login.";
            return View();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        _tokenProvider.ClearToken();
        return RedirectToAction("Finder", "GitHubFinder");
    }

    [HttpGet]
    public async Task<IActionResult> Register()
    {
        SetRoleList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegistrationRequestDto model)
    {
        ResponseDto result = await _authService.RegisterAsync(model);
        ResponseDto assingRole;

        if (result.IsSuccess && result != null)
        {
            if (string.IsNullOrEmpty(model.Role))
            {
                model.Role = SD.COSTUMER;
            }

            assingRole = await _authService.AssignRoleAsync(model);

            if (assingRole.IsSuccess && assingRole != null)
            {
                TempData["success"] = "Registration seccessful";
                return RedirectToAction(nameof(Login));
            }
        }
        else
        {
            TempData["error"] = result?.Message ?? "An error occurred during registration.";
        }

        SetRoleList();
        return View();
    }

    #region user authentication
    private async Task SingInUser(LoginResponseDto model)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(model.Token);

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

        identity.AddClaim(new Claim(ClaimTypes.Name,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));
        identity.AddClaim(new Claim(ClaimTypes.Role,
            jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }

    private void SetRoleList()
    {
        var roleList = new List<SelectListItem>()
        {
            new SelectListItem(){Text = SD.ADMINISTRATOR, Value = SD.ADMINISTRATOR},
            new SelectListItem(){Text = SD.COSTUMER, Value = SD.COSTUMER}
        };

        ViewBag.RoleList = roleList;
    }
    #endregion
}