using Microsoft.AspNetCore.Identity;

namespace Forms.Services;

public interface IAuthService
{
    Task<SignInResult> SignIn(string email, string password);
    Task<IdentityResult> SignUp(string name, string email, string password);
    Task Logout();
}
