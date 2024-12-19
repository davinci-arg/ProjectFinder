using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ProjectFinder.Web.Models;
using ProjectFinder.Web.Service.IService;

namespace ProjectFinder.Web.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
    {
        await _authService.Login(loginRequestDto);
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
    {
        await _authService.Register(registrationRequestDto);
        return View();
    }
}
