using AutoMapper;
using hospital__management_system.Core.Constants;
using hospital__management_system.Core.Dtos;
using hospital__management_system.Core.Dtos.Account;
using hospital__management_system.Core.Enums;
using hospital__management_system.Core.Helpers;
using hospital__management_system.Core.Interfaces;
using hospital__management_system.Core.Interfaces.Services;
using hospital__management_system.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;

namespace hospital__management_system.EF.Services;
public class AccountService : IAccountService
{
    private readonly IMapper _mapper;



    private readonly IUnitOfWork _unitOfWork;
    private UserManager<ApplicationUser> _userManager;
    private ITokenGenerator _tokenGenerator;
    private readonly HttpClient _httpClient;
    private readonly IEmailService _emailService;

    public AccountService(
            UserManager<ApplicationUser> userManager,
            ITokenGenerator tokenGenerator,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            HttpClient httpClient  ,
            IEmailService emailService
 

            )
    {
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpClient = httpClient;
        _emailService = emailService;
    }

    public async Task<ResponseDTO> LoginUser(UserLoginDTO dto)
    {
        ApplicationUser appUser = await _userManager.FindByEmailAsync(dto.Email);
        if (appUser is null) { return new ResponseDTO(ResponseStatusCode.NotFound, false, null, "User not found"); }
        var result = await _userManager.CheckPasswordAsync(appUser, dto.Password);
        if (!result) { return new ResponseDTO(ResponseStatusCode.InCorrectPassword, false, null, "In Correct Password"); }

       

        return new ResponseDTO(ResponseStatusCode.Ok, true, new UserDto
        {

            Name = $"{appUser.UserName}",
            Token = await _tokenGenerator.GenerateToken(new AppUserTokenGeneratorDTO() { Email=appUser.Email,Name=appUser.UserName,ID=appUser.Id,User=appUser}),
        });
    }
    public async Task<ResponseDTO> UserRegister(UserRegisterDTO UserDto)
    {
        if (UserDto.UserType == "Patient")
        {
            return await PatientRegister(UserDto);
        }
        if (UserDto.UserType == "Admin")
        {
            return await AdminRegister(UserDto);
        }
        else
        {
            return new ResponseDTO(ResponseStatusCode.Ok, true, null, "Doctor Register Not Implemented");
        }

    }

    public async Task<ResponseDTO> AdminRegister(UserRegisterDTO userDto)
    {
        ApplicationUser appUser = _mapper.Map<ApplicationUser>(userDto);
        appUser.UserName = userDto.UserName;
      
        ResponseDTO appUserResponse = ValidateUserRegister(appUser);
        if (!appUserResponse.Success) { return appUserResponse; }

        return await CompleteRegister(new BasicRegisterDTO() { AppUser = appUser, Password = userDto.Password, UserName = userDto.UserName }, Roles.Admin);
    }

    public async Task<ResponseDTO> PatientRegister(UserRegisterDTO UserDto)
    {
        ApplicationUser appUser = _mapper.Map<ApplicationUser>(UserDto);
        Patient patient = _mapper.Map<Patient>(UserDto);
      
        appUser.UserName = UserDto.UserName;
        appUser.Patient = patient;
        patient.ApplicationUser = appUser;

        

        ResponseDTO appUserResponse = ValidateUserRegister(appUser);
        if (!appUserResponse.Success) { return appUserResponse; }
        
        ResponseDTO patientResponse = ValidatePatientRegister(patient);
        if (!patientResponse.Success) { return patientResponse; }

        
            return await CompleteRegister(new BasicRegisterDTO() { AppUser = appUser, Password = UserDto.Password, UserName = UserDto.UserName }, Roles.Patient);

        
    }
    public async Task<ResponseDTO> ForgotPassword(ForgetPasswordRequestDTO model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return new ResponseDTO(ResponseStatusCode.NotFound, true, null, "User Not Found");

        }

        var token = _tokenGenerator.GenerateRandomToken(); 

       
        var resetUrl = new UriBuilder("https://localhost:7152/api/Account/ResetPassword?")
        {
            Query = $"email={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(token)}"
        }.ToString();
        var emailBody = $"Click the following link to reset your password: {resetUrl}";

        try
        {
            await _emailService.SendEmailAsync(user.Email, "Password Reset", emailBody);

        }
        catch (Exception ex)
        {

            return new ResponseDTO(ResponseStatusCode.Ok, true, null, ex.ToString());

        }



        return new ResponseDTO(ResponseStatusCode.Ok, true, null, "Check Your Email");
    }
    public async Task<ResponseDTO> ResetPassword(ResetPasswordRequestDTO model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            new ResponseDTO(ResponseStatusCode.NotFound, true, null, "User Not Found");
            // Email not found, return an error message

        }
        // Decode and validate the token
        var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
        var decodedTokenString = Encoding.UTF8.GetString(decodedToken);
        var isValidToken = await _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider, "ResetPassword", decodedTokenString);

        if (!isValidToken)
        {
            // Invalid or expired token, return an error message
            return new ResponseDTO(ResponseStatusCode.UnSuccessfullAuthenticarion, true, null, "Invalid Token");
        }

        // Find the user based on the email address
        

        // Reset the user's password
        var resetPasswordResult = await _userManager.ResetPasswordAsync(user, decodedTokenString, model.NewPassword);
        if (!resetPasswordResult.Succeeded)
        {
            // Failed to reset the password, return an error message
            return new ResponseDTO(ResponseStatusCode.InValidModel, true, null, "Failed To Reset Password");
        }

        // Password reset successful, return a success message
        return new ResponseDTO(ResponseStatusCode.Ok, true, null, "Password Reset Successfully");
    }


    private async Task<ResponseDTO> CompleteRegister(BasicRegisterDTO registerDTO,string Role)
    {
        IdentityResult result;
        try
        {
            result = await _userManager.CreateAsync(registerDTO.AppUser, registerDTO.Password);
        }
        catch (Exception ex)
        {
            return new ResponseDTO(ResponseStatusCode.InValidModel, false, null, ex.Message.ToString());
        }

        if (!result.Succeeded)
        { return new ResponseDTO(ResponseStatusCode.UnSuccessfullAuthenticarion, false, result.Errors); }

        await _userManager.AddToRoleAsync(registerDTO.AppUser, Role);

        return new ResponseDTO(ResponseStatusCode.Ok, true, new UserDto
        {
            Name = registerDTO.UserName,
            Token = await _tokenGenerator.GenerateToken(new AppUserTokenGeneratorDTO() { Email=registerDTO.AppUser.Email,ID=registerDTO.AppUser.Id,Name=registerDTO.AppUser.UserName,User=registerDTO.AppUser}),
        });
    }

    

    private ResponseDTO ValidateUserRegister(ApplicationUser appUser)
    {
        if (false)
        {
            new ResponseDTO(ResponseStatusCode.InValidModel, false, null); ;
        }
        return new ResponseDTO(ResponseStatusCode.Ok, true, null);
    }

    

    private ResponseDTO ValidatePatientRegister(Patient patient)
    {
        if (false)
        {
            new ResponseDTO(ResponseStatusCode.InValidModel, false, null); ;
        }
        return new ResponseDTO(ResponseStatusCode.Ok, true, null);
    }

  
   

}
