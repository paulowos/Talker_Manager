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

    [HttpGet(":id")]
    public ActionResult<Talker?> GetById(int id)
    {
        var result = _repository.GetById(id);
        return result is null ? NotFound(new { message = "Pessoa palestrante não encontrada" }) : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Talker>> Add(TalkerDTO talkerDTO)
    {
        var result = await _repository.Add(talkerDTO);
        return Created("", result);
    }
}