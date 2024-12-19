using ProjectFinder.Web.Models;
using ProjectFinder.Web.Service.IService;
using ProjectFinder.Web.Utility;

namespace ProjectFinder.Web.Service;

public class AuthService : IAuthService
{
    private readonly IBaseService _baseService;

    public AuthService(IEnumerable<IBaseService> services)
    {
        _baseService = services.First(service => service is BaseService);
    }

    public async Task<ResponseDto> AssignRole(RegistrationRequestDto registrationRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Data = registrationRequestDto,
            Url = SD.AuthAPIService + "/api/auth/assign/"
        });
    }

    public async Task<ResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Data = loginRequestDto,
            Url = SD.AuthAPIService + "/api/auth/login/"
        });
    }

    public async Task<ResponseDto> Register(RegistrationRequestDto registrationRequestDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Data = registrationRequestDto,
            Url = SD.AuthAPIService + "/api/auth/register/"
        });
    }
}
