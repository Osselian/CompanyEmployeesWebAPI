using Microsoft.AspNetCore.Identity;
using Shared.DataTranferObjects;

namespace Service.Contracts
{
    public interface IAutheticationService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<TokenDto> CreateToken(bool populateExp);
        Task<TokenDto> RefreshToken(TokenDto token);
    }
}
