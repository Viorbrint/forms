using Forms.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService)
{
    [HttpPost("signin")]
    public async Task<IResult> Signin(SigninModel model)
    {
        var result = await authService.Signin(model.Email, model.Password);
        if (result.Succeeded)
        {
            return Results.Ok(new { message = "Signin successful" });
        }
        else if (result.IsLockedOut)
        {
            return Results.Problem(
                "User account is locked out",
                statusCode: StatusCodes.Status401Unauthorized
            );
        }
        else
        {
            return Results.Problem(
                "Invalid email or password",
                statusCode: StatusCodes.Status401Unauthorized
            );
        }
    }
}
