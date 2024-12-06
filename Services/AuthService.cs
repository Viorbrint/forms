using Microsoft.AspNetCore.Identity;

namespace Forms.Services;

public class IdentityAuthService : IAuthService
{
    public Task Logout()
    {
        throw new NotImplementedException();
    }

    public Task<SignInResult> SignIn(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> SignUp(string name, string email, string password)
    {
        throw new NotImplementedException();
    }
}
