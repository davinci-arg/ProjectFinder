using Service.AuthAPI.Models.Dto;

namespace Service.AuthAPI.Service.IService;

public interface IAuthService
{
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    Task<string> Register(RegistrationRequestDto registrationRequestDto);
}
