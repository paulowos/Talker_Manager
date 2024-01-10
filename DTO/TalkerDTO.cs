using System.ComponentModel.DataAnnotations;
using TalkerManager.Model;

namespace TalkerManager.DTO;

public class TalkerDTO
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string? Name { get; set; }

    [Required] [Range(18, int.MaxValue)] public int? Age { get; set; }

    public Talk? Talk { get; set; }
}