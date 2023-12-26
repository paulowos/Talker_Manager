using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using TalkerManager.DTO;
using TalkerManager.Model;

namespace TalkerManager.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class TalkerController : ControllerBase
{
    private readonly IRepository _repository;

    public TalkerController(IRepository repository)
    {
        _repository = repository;
    }

    /// <summary> Get all Talkers </summary>
    /// <response code="200"> Returns all Talkers </response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Talker[]> GetAll()
    {
        var result = _repository.GetAll();
        return Ok(result);
    }

    /// <summary> Get Talker by Id </summary>
    /// <response code="200"> Returns a Talker </response>
    /// <response code="404"> Talker not found </response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status404NotFound)]
    public ActionResult<Talker?> GetById(int id)
    {
        var result = _repository.GetById(id);
        return result is null
            ? NotFound(new ErrorMessage { message = "Pessoa palestrante não encontrada" })
            : Ok(result);
    }
    /// <summary> Get talkers by query </summary>
    /// <param name="authorization">API Token</param>
    /// <param name="q">Talker Name</param>
    /// <param name="rate">Talk Rate</param>
    /// <param name="date">Talk Date</param>
    /// <response code="200"> Returns Talkers that match query </response>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Talker[]> GetByQuery([FromHeader] string authorization, string? q = "", int? rate = null,
        DateTime? date = null)
    {
        _ = AuthenticationHeaderValue.Parse(authorization);
        var result = _repository.GetByQuery(q, rate, date);
        return Ok(result);
    }

    /// <summary> Create new Talker </summary>
    /// <param name="authorization">API Token</param>
    /// <param name="talkerDTO">Talker Info</param>
    /// <response code="201"> Returns the created Talker </response>
    [HttpPost]
    [ProducesResponseType( StatusCodes.Status201Created)]
    public async Task<ActionResult<Talker>> Add([FromHeader] string authorization, TalkerDTO talkerDTO)
    {
        _ = AuthenticationHeaderValue.Parse(authorization);
        var result = await _repository.Add(talkerDTO);
        return Created("", result);
    }

    /// <summary> Update Talker by Id </summary>
    /// <param name="authorization">API Token</param>
    /// <param name="id">Talker Id</param>
    /// <param name="talkerDTO">Talker Info</param>
    /// <response code="200"> Returns the updated Talker </response>
    /// <response code="404"> Talker not found </response>
    [HttpPut("{id:int}")]
    [ProducesResponseType( StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Talker?>> UpdateById([FromHeader] string authorization, [FromRoute] int id,
        [FromBody] TalkerDTO talkerDTO)
    {
        _ = AuthenticationHeaderValue.Parse(authorization);
        var result = await _repository.Update(id, talkerDTO);
        if (!result) return NotFound(new ErrorMessage { message = "Id not found" });
        return new Talker
        {
            Id = id,
            Name = talkerDTO.Name,
            Age = talkerDTO.Age,
            Talk = talkerDTO.Talk
        };
    }

    /// <summary> Delete Talker by Id </summary>
    /// <param name="authorization">API Token</param>
    /// <param name="id">Talker Id</param>
    /// <response code="204"> Talker deleted </response>
    /// <response code="404"> Talker not found </response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete([FromHeader] string authorization, [FromRoute] int id)
    {
        _ = AuthenticationHeaderValue.Parse(authorization);
        var result = await _repository.Delete(id);
        if (!result) return NotFound(new ErrorMessage { message = "Id not found" });
        return NoContent();
    }
}