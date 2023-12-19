using JsonFlatFileDataStore;
using TalkerManager.Model;

namespace TalkerManager.Data;

public class Data
{
    private readonly DataStore _store = new("talker.json", true, "id");
    private IDocumentCollection<Talker> _collection;

    public Data()
    {
        _collection = _store.GetCollection<Talker>();
    }
}