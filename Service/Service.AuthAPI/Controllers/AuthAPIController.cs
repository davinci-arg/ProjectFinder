using Microsoft.AspNetCore.Mvc;
using Service.AuthAPI.Models.Dto;
using Service.AuthAPI.Service.IService;

namespace Service.AuthAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthAPIController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ResponseDto _responseDto;

    public AuthAPIController(IAuthService authService)
    {
        _authService = authService;
        _responseDto = new ResponseDto();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
    {
        var result = await _authService.Register(model);

        if (!string.IsNullOrEmpty(result))
        {
            _responseDto.Success = false;
            _responseDto.Message = result;
            return BadRequest(_responseDto);
        }

        return Ok(_responseDto);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        var loginResponse = await _authService.Login(model);

        if (loginResponse.User == null)
        {
            _responseDto.Success = false;
            _responseDto.Message = "Username или password введены неверно";
            return BadRequest(_responseDto);
        }

        _responseDto.Result = loginResponse;
        return Ok(_responseDto);
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
    {
        var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());

        if (assignRoleSuccessful)
        {
            _responseDto.Success = false;
            _responseDto.Message = "Error encountered";
            return BadRequest(_responseDto);
        }

        _responseDto.Result = assignRoleSuccessful;
        return Ok(_responseDto);
    }



}
