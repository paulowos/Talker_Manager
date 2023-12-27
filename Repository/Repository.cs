using JsonFlatFileDataStore;
using TalkerManager.DTO;
using TalkerManager.Model;

namespace TalkerManager.Repository;

public class Repository : IRepository
{
    private readonly IDocumentCollection<Talker> _collection;
    private readonly DataStore _store = new("talker.json", true, "id");

    public Repository()
    {
        _collection = _store.GetCollection<Talker>();
    }

    public IEnumerable<Talker> GetAll()
    {
        var result = _collection.AsQueryable().Where(_ => true);
        return result;
    }

    public Talker? GetById(int id)
    {
        var result = _collection.AsQueryable().FirstOrDefault(t => t.Id == id);
        return result;
    }

    public async Task<Talker> Add(TalkerDTO talkerDTO)
    {
        var id = _collection.GetNextIdValue();
        var talker = new Talker
        {
            Id = id,
            Name = talkerDTO.Name,
            Age = talkerDTO.Age,
            Talk = talkerDTO.Talk
        };
        await _collection.InsertOneAsync(talker);
        return talker;
    }

    public async Task<bool> Update(int id, TalkerDTO talkerDTO)
    {
        var updated = await _collection.UpdateOneAsync(id, talkerDTO);
        return updated;
    }

    public async Task<bool> Delete(int id)
    {
        var deleted = await _collection.DeleteOneAsync(id);
        return deleted;
    }

    public IEnumerable<Talker> GetByQuery(string q, int? rate, DateTime? date)
    {
        var result = _collection.AsQueryable().Where(t => t.Name!.ToUpper().Contains(q.ToUpper()));
        if (rate is not null) result = result.Where(t => t.Talk!.Rate == rate);
        if (date is not null) result = result.Where(t => t.Talk!.WatchedAt.Equals(date));
        return result;
    }
}