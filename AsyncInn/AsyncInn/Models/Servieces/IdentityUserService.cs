using AsyncInn.Models.DTO;
using AsyncInn.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AsyncInn.Models.Servieces
{
    public class IdentityUserService : IUserService
    {
        // Connect to Identity’s “User Manager” to do the database work
        private  UserManager<ApplicationUser> _userManager;
        private JwtTokenService tokenService;

        // ApplicationUser we want to manage it
        public IdentityUserService(UserManager<ApplicationUser> manager, JwtTokenService jwtTokenService)
        {
            _userManager = manager;
            tokenService = jwtTokenService;

        }
        public async Task<UserDto> Authenticate(string username, string password)
        {
            // FindByNameAsync Finds and returns a user, if any, who has the specified user name.
            // resource: https://stackoverflow.com/questions/55149535/usermanager-checkpasswordasync-always-returns-failure
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                //check if password is correct
                //PasswordVerificationResult result = _userManager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, password);
               if (await _userManager.CheckPasswordAsync(user, password))
                {
                    return new UserDto
                    {
                        Username = user.UserName,
                        Token = await tokenService.GetToken(user, System.TimeSpan.FromDays(2)),
                        Roles = await _userManager.GetRolesAsync(user)
                    }; 
                }
                //var user = UserManager.FindByNameAsync(username);
                //return SignInManager.UserManager.CheckPassword(user, Password);
            }
            return null;
        }
        // RegisterUserDto data means take data from body, will take all fields
        public async Task<UserDto> Register(RegisterUser data, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser
            {
                UserName = data.Username,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, data.Password);
            // CreateAsync : create user, and password hash password and save it in database
            if (result.Succeeded)
            {
                // here goes the roles specifications ... 
                IList<string> Roles = new List<string>();
                Roles.Add("user");
                await _userManager.AddToRolesAsync(user, Roles);

                return new UserDto
                {
                    Username = user.UserName,
                    Token = await tokenService.GetToken(user, System.TimeSpan.FromDays(2)),
                    Roles = await _userManager.GetRolesAsync(user)
                };
            }
            foreach (var error in result.Errors)
            {
                var errorKey =
                    // nameof will go to the RegisterUser class and take property name 
                    error.Code.Contains("Password") ? /* key name will be -> */nameof(data.Password) :
                    error.Code.Contains("Email") ? /* key name will be -> */nameof(data.Email) :
                    error.Code.Contains("UserName") ? /* key name will be -> */nameof(data.Username) :
                    "";
                modelState.AddModelError(errorKey, error.Description);
            }
            return null;
        }
        public async Task<UserDto> GetUser(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);
            return new UserDto
            {
                Username = user.UserName,
            };
        }
    }
}

// any post request accept data inbound(sth in body) get data from client,
// modelState will submitted to server automatically during post request and it
// will represent validation errors. modelState is a collection of pairs(name and value) 