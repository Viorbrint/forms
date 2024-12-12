using Microsoft.AspNetCore.Identity;

namespace Forms.Services;

public interface IAuthService
{
    Task<SignInResult> CheckSignin(string email, string password);
    Task<SignInResult> Signin(string email, string password);
    Task<IdentityResult> Signup(string name, string email, string password);
    Task Logout();
}
