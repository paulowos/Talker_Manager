using System.Net.Http.Headers;
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

    [HttpGet("{id:int}")]
    public ActionResult<Talker?> GetById(int id)
    {
        var result = _repository.GetById(id);
        return result is null ? NotFound(new { message = "Pessoa palestrante não encontrada" }) : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Talker>> Add([FromHeader] string authorization, TalkerDTO talkerDTO)
    {
        _ = AuthenticationHeaderValue.Parse(authorization);
        var result = await _repository.Add(talkerDTO);
        return Created("", result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Talker?>> UpdateById([FromHeader] string authorization, [FromRoute] int id,
        [FromBody] TalkerDTO talkerDTO)
    {
        _ = AuthenticationHeaderValue.Parse(authorization);
        var result = await _repository.Update(id, talkerDTO);
        if (!result) return NotFound(new { message = "Id not found" });
        return new Talker
        {
            Id = id,
            Name = talkerDTO.Name,
            Age = talkerDTO.Age,
            Talk = talkerDTO.Talk
        };
    }
}