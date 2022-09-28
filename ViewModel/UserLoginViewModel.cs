using System.ComponentModel.DataAnnotations;

namespace ViewModel;

public class UserLoginViewModel
{
    [Required(ErrorMessage ="E-mail Obrigatório")]
    public string Email { get; set; }

    [Required(ErrorMessage ="Senha Obrigatória")]
    public string Password { get; set; }
}