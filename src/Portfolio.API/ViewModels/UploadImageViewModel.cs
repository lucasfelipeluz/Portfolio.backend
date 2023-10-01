using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.ViewModels
{
  public class UploadImageViewModel
  {
    [Required(ErrorMessage = "The image base64 is required!")]
    [MinLength(3, ErrorMessage = "The image base64 must have more than 3 letters")]
    public string Base64 { get; set; }

    public int? ProjectId { get; set; }
    public int? SkillId { get; set; }
  }
}

