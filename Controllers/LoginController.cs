using Microsoft.AspNetCore.Mvc;
using TalkerManager.DTO;
using TalkerManager.Model;

namespace TalkerManager.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class LoginController : ControllerBase
{
    /// <summary> Login to the API </summary>
    /// <response code="200"> Returns a Token </response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<string> Login(LoginDTO loginDTO)
    {
        var token = new Token(loginDTO.Email, loginDTO.Password);
        return Ok(token);
    }
}