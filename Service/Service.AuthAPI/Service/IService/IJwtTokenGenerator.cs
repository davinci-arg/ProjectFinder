using Service.AuthAPI.Models;

namespace Service.AuthAPI.Service.IService;

public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser applicationUser);
}
