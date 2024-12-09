using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PingController
{
    [HttpGet]
    public IResult Ping()
    {
        return Results.Ok("Pong");
    }
}
