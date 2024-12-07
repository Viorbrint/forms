using Microsoft.AspNetCore.Identity;

namespace Forms.Services;

public class IdentityAuthService : IAuthService
{
    public Task Logout()
    {
        throw new NotImplementedException();
    }

    public async Task<SignInResult> Signin(string email, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<IdentityResult> Signup(string name, string email, string password)
    {
        throw new NotImplementedException();
    }
}
