using System.ComponentModel.DataAnnotations;

namespace TalkerManager.Model;

public class Talk
{
    [Required] [DataType(DataType.Date)] public DateTime WatchedAt { get; set; }

    [Required] [Range(0, 5)] public int Rate { get; set; }
}