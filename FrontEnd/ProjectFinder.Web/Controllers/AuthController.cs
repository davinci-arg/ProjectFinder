using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectFinder.Web.Models;
using ProjectFinder.Web.Service.IService;
using ProjectFinder.Web.Utility;

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
    public async Task<IActionResult> Register()
    {
        var roleList = new List<SelectListItem>()
        {
            new SelectListItem(){Text = SD.ADMINISTRATOR, Value = SD.ADMINISTRATOR},
            new SelectListItem(){Text = SD.COSTUMER, Value = SD.COSTUMER}
        };

        ViewBag.RoleList = roleList;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegistrationRequestDto model)
    {
        ResponseDto responseRegister = await _authService.Register(model);
        ResponseDto assingRole;

        if (responseRegister.IsSuccess && responseRegister != null)
        {
            if (string.IsNullOrEmpty(model.Role))
            {
                model.Role = SD.COSTUMER;
            }

            assingRole = await _authService.AssignRole(model);

            if (assingRole.IsSuccess && assingRole != null)
            {
                TempData["success"] = "Registration seccessful";
                return RedirectToAction(nameof(Login));
            }
        }

        var roleList = new List<SelectListItem>()
        {
            new SelectListItem(){Text = SD.ADMINISTRATOR, Value = SD.ADMINISTRATOR},
            new SelectListItem(){Text = SD.COSTUMER, Value = SD.COSTUMER}
        };

        ViewBag.RoleList = roleList;

        return View();
    }
}
