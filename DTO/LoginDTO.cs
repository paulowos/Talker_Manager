using System.ComponentModel.DataAnnotations;

namespace TalkerManager.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "O campo \"email\" é obrigatório")]
    [EmailAddress(ErrorMessage = "O \"email\" deve ser válido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo \"password\" é obrigatório")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "O \"password\" possui tamanho inválido")]
    public string Password { get; set; }
}