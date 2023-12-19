using TalkerManager.Model;

namespace TalkerManager.DTO;

public class TalkerDTO
{
    public string? Name { get; set; }
    public int? Age { get; set; }
    public Talk? Talk { get; set; }
}