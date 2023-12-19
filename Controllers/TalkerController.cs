using Microsoft.AspNetCore.Mvc;
using TalkerManager.DTO;
using TalkerManager.Model;

namespace TalkerManager.Controllers;

[ApiController]
[Route("[controller]")]
public class TalkerController : ControllerBase
{
    private readonly IRepository _repository;

    public TalkerController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<Talker[]> GetAll()
    {
        var result = _repository.GetAll();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Talker>> Add([FromBody] TalkerDTO talkerDTO)
    {
        var result = await _repository.Add(talkerDTO);
        return Created("", result);
    }
}