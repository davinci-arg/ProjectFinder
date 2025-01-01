using Microsoft.AspNetCore.Identity;
using Service.AuthAPI.Data;
using Service.AuthAPI.Models;
using Service.AuthAPI.Models.Dto;
using Service.AuthAPI.Service.IService;

namespace Service.AuthAPI.Service;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(AppDbContext db, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _jwtTokenGenerator = jwtTokenGenerator;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<bool> AssignRole(string email, string roleName)
    {
        var user = _db.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

        if (user != null)
        {
            if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            await _userManager.AddToRoleAsync(user, roleName);
            return true;
        }
        return false;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var user = _db.Users.FirstOrDefault(u => u.Email.ToLower() == loginRequestDto.Email.ToLower());
        bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

        if (user == null || isValid == false)
        {
            return new LoginResponseDto() { User = null, Token = "" };
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtTokenGenerator.GenerateToken(user, roles);

        UserDto userDto = new UserDto()
        {
            Email = user.Email,
            ID = user.Id,
            Name = user.Name
        };

        return new LoginResponseDto()
        {
            User = userDto,
            Token = token
        };
    }

    public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
    {
        ApplicationUser user = new()
        {
            UserName = registrationRequestDto.Email,
            Email = registrationRequestDto.Email,
            NormalizedEmail = registrationRequestDto.Email.ToUpper(),
            Name = registrationRequestDto.Name,
        };

        try
        {
            var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);

            if (result.Succeeded)
            {
                var userToReturn = _db.Users.First(u => u.Email == registrationRequestDto.Email);

                UserDto userDto = new()
                {
                    Email = userToReturn.Email,
                    Name = userToReturn.Name
                };

                return "";
            }
            else return result.Errors.FirstOrDefault().Description;


        }
        catch (Exception ex)
        {

        }

        return "Error Encounted";
    }
}
