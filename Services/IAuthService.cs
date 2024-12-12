using Forms.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Forms.Services;

public interface IAuthService
{
    Task<SignInResult> CheckSignin(User user, string password);
    Task<SignInResult> Signin(User user, string password);
    Task<IdentityResult> Signup(string name, string email, string password);
    Task Logout();
}
