using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.ViewModels
{
  public class RegisterViewModel
  {
    [MinLength(3, ErrorMessage = "The user name must have more than 3 letters")]
    [MaxLength(80, ErrorMessage = "The user name must have less than 80 letters")]
    public string Name { get; set; }

    [MinLength(3, ErrorMessage = "The user text password have more than 3 letters")]
    [MaxLength(80, ErrorMessage = "The user text password have less than 80 letters")]
    public string Password { get; set; }

    [MinLength(3, ErrorMessage = "The user nickname must have more than 3 letters")]
    [MaxLength(80, ErrorMessage = "The user nickname must have less than 80 letters")]
    public string NickName { get; set; }

  }
}
