using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.ViewModels
{
  public class CreateProjectViewModel
  {
    [Required(ErrorMessage = "The project description is required!")]
    [MinLength(3, ErrorMessage = "The project description must have more than 3 letters")]
    [MaxLength(80, ErrorMessage = "The project description must have less than 80 letters")]
    public string Title { get; set; }

    [Required(ErrorMessage = "The project description is required!")]
    [MinLength(3, ErrorMessage = "The project description must have more than 3 letters")]
    [MaxLength(80, ErrorMessage = "The project description must have less than 80 letters")]
    public string Description { get;  set; }
    public string UrlWebsite { get; set; }

    [Required(ErrorMessage = "The project description is required!")]
    [MinLength(3, ErrorMessage = "The project description must have more than 3 letters")]
    [MaxLength(80, ErrorMessage = "The project description must have less than 80 letters")]
    public string UrlGithub { get;  set; }

    [Required(ErrorMessage = "The project view priority is required!")]
    public int ViewPriority { get;  set; }

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "The project Start Date is required!")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime? StartedAt { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime? FinishedAt { get; set; }
  }
}
