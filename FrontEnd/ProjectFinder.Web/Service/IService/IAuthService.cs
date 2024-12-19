using ProjectFinder.Web.Models;

namespace ProjectFinder.Web.Service.IService;

public interface IAuthService
{
    Task<ResponseDto> Login(LoginRequestDto loginRequestDto);
    Task<ResponseDto> Register(RegistrationRequestDto registrationRequestDto);
    Task<ResponseDto> AssignRole(RegistrationRequestDto registrationRequestDto);
}
