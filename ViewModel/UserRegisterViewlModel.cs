using System.ComponentModel.DataAnnotations;

namespace ViewModel;

public class UserRegisterViewlModel
{
    [Required(ErrorMessage = "O Nome é Obrigatório")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O Nome é Obrigatório")]
    [EmailAddress(ErrorMessage ="O E-mail é inválido")]
    public string Email { get; set; }
}