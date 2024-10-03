using Microsoft.AspNetCore.Identity;
using Shared.DataTranferObjects;

namespace Service.Contracts
{
    public interface IAutheticationService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
    }
}
