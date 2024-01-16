using hospital__management_system.Core.Dtos;
using hospital__management_system.Core.Dtos.Account;
using hospital__management_system.Core.Enums;
using hospital__management_system.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace hospital__management_system.Core.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<ResponseDTO>> Login(UserLoginDTO dto)
    {
        if (!ModelState.IsValid) { return BadRequest(new ResponseDTO(ResponseStatusCode.InValidModel, false, ModelState)); };

        return Ok(await _accountService.LoginUser(dto));
    }

    [HttpPost("Register")]
    public async Task<ActionResult<ResponseDTO>> UserRegister(UserRegisterDTO dto)
    {
        if (!ModelState.IsValid) 
        { return BadRequest(new ResponseDTO(ResponseStatusCode.InValidModel, false, ModelState)); };

        return Ok(await _accountService.UserRegister(dto));
    }

    [HttpPost("ForgetPassword")]
    public async Task<ActionResult<ResponseDTO>> ForgetPassword(ForgetPasswordRequestDTO dto)
    {
        if (!ModelState.IsValid)
        { return BadRequest(new ResponseDTO(ResponseStatusCode.InValidModel, false, ModelState)); };

        return Ok(await _accountService.ForgotPassword(dto));
    }
    [HttpPost("ResetPassword")]
    public async Task<ActionResult<ResponseDTO>> ResetPassword(ResetPasswordRequestDTO dto)
    {
        if (!ModelState.IsValid)
        { return BadRequest(new ResponseDTO(ResponseStatusCode.InValidModel, false, ModelState)); };

        return Ok(await _accountService.ResetPassword(dto));
    }




}
