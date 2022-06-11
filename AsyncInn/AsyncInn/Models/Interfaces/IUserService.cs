using AsyncInn.Models.Api;
using AsyncInn.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using System.Threading.Tasks;
using UserDto = AsyncInn.Models.DTO.UserDto;

namespace AsyncInn.Models.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> Register(RegisterUser data, ModelStateDictionary modelState);
        public Task<UserDto> Authenticate(string username, string password);
        public Task<UserDto> GetUser(ClaimsPrincipal principal);

    }
}
