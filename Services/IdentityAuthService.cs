using Forms.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Forms.Services;

public class IdentityAuthService(
    SignInManager<User> signInManager,
    UserManager<User> userManager,
    ILogger<IdentityAuthService> logger
) : IAuthService
{
    public async Task Logout()
    {
        await signInManager.SignOutAsync();
        logger.LogInformation("User logged out.");
    }

    public async Task<SignInResult> CheckSignin(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return SignInResult.Failed;
        }
        var result = await signInManager.CheckPasswordSignInAsync(
            user,
            password,
            lockoutOnFailure: false
        );
        return result;
    }

    // TODO: fix that login queries db two times
    public async Task<SignInResult> Signin(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return SignInResult.Failed;
        }
        var result = await signInManager.PasswordSignInAsync(
            user,
            password,
            isPersistent: true,
            lockoutOnFailure: false
        );
        if (result.Succeeded)
        {
            logger.LogInformation("User logged in.");
        }
        return result;
    }

    public async Task<IdentityResult> Signup(string name, string email, string password)
    {
        logger.LogInformation(
            $"Trying to register user with email {email} and password {password}"
        );
        var user = new User { UserName = name, Email = email };
        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            logger.LogInformation("User created a new account");
        }
        return result;
    }
}
