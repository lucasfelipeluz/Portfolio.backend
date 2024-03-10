using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.ViewModels;

public class CreateProjectViewModel
{
	[Required(ErrorMessage = "The project title is required!")]
	[MinLength(3, ErrorMessage = "The project title must have more than 3 letters")]
	[MaxLength(100, ErrorMessage = "The project title must have less than 100 letters")]
	public string Title { get; set; }

	[Required(ErrorMessage = "The project title in english is required!")]
	[MinLength(3, ErrorMessage = "The project title in english must have more than 3 letters")]
	[MaxLength(100, ErrorMessage = "The project title in english must have less than 100 letters")]
	public string TitleEnglish { get; set; }

	[Required(ErrorMessage = "The project description is required!")]
	[MinLength(3, ErrorMessage = "The project description must have more than 3 letters")]
	[MaxLength(500, ErrorMessage = "The project description must have less than 500 letters")]
	public string Description { get; set; }

	[Required(ErrorMessage = "The project description in english is required!")]
	[MinLength(3, ErrorMessage = "The project description in english must have more than 3 letters")]
	[MaxLength(500, ErrorMessage = "The project description in english must have less than 500 letters")]
	public string DescriptionEnglish { get; set; }
	public string UrlWebsite { get; set; }

	[Required(ErrorMessage = "The project url github is required!")]
	[MinLength(3, ErrorMessage = "The project url github must have more than 3 letters")]
	[MaxLength(80, ErrorMessage = "The project url github must have less than 80 letters")]
	public string UrlGithub { get; set; }

	[Required(ErrorMessage = "The project view priority is required!")]
	public int ViewPriority { get; set; }

	[DataType(DataType.Date)]
	[Required(ErrorMessage = "The project Start Date is required!")]
	[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
	public DateTime? StartedAt { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
	public DateTime? FinishedAt { get; set; }
}
