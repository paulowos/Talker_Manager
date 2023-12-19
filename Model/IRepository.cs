using TalkerManager.DTO;

namespace TalkerManager.Model;

public interface IRepository
{
    IEnumerable<Talker> GetAll();
    Talker? GetById(int id);
    Task<Talker> Add(TalkerDTO talkerDTO);
}