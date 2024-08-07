using DemoProje.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProje.Core.Contracts
{
    public interface IUser
    {
        Task<RegistrationResponse> RegiserUserAsync(RegisterUserDTO registerUserDTO);
        Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO);
    }
}
