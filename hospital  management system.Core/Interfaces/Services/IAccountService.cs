using hospital__management_system.Core.Dtos;
using hospital__management_system.Core.Dtos.Account;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.Interfaces.Services
{
    public interface IAccountService
    {
        Task<ResponseDTO> LoginUser(UserLoginDTO dto);
        Task<ResponseDTO> UserRegister(UserRegisterDTO patientDto);
        Task<ResponseDTO> PatientRegister(UserRegisterDTO UserDto);
        Task<ResponseDTO> ForgotPassword(ForgetPasswordRequestDTO model);
        Task<ResponseDTO> ResetPassword(ResetPasswordRequestDTO model);
    }
}
