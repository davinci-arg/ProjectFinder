using Microsoft.AspNetCore.Identity;

namespace Service.AuthAPI.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set;}
}
