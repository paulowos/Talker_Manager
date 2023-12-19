using Microsoft.AspNetCore.Mvc;
using TalkerManager.DTO;
using TalkerManager.Model;

namespace TalkerManager.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    [HttpPost]
    public ActionResult<string> Login(LoginDTO loginDTO)
    {
        var token = new Token(loginDTO.Email, loginDTO.Password);
        return Ok(token);
    }
}