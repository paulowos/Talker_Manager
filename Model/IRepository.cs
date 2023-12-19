using TalkerManager.DTO;

namespace TalkerManager.Model;

public interface IRepository
{
    IEnumerable<Talker> GetAll();
    Task<Talker> Add(TalkerDTO talkerDTO);
}