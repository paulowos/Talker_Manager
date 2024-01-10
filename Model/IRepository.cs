using TalkerManager.DTO;

namespace TalkerManager.Model;

public interface IRepository
{
    IEnumerable<Talker> GetAll();
    Talker? GetById(int id);
    Task<Talker> Add(TalkerDTO talkerDTO);
    Task<bool> Update(int id, TalkerDTO talkerDTO);
    Task<bool> Delete(int id);
    IEnumerable<Talker> GetByQuery(string q, int? rate, DateTime? date);
}