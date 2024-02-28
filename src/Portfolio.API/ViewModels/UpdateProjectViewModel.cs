using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.ViewModels;

public class UpdateProjectViewModel
{
	[Required(ErrorMessage = "The project Id is required!")]
	public int Id { get; set; }

	[MinLength(3, ErrorMessage = "The project description must have more than 3 letters")]
	[MaxLength(80, ErrorMessage = "The project description must have less than 80 letters")]
	public string Title { get; set; }

	[MinLength(3, ErrorMessage = "The project description must have more than 3 letters")]
	[MaxLength(80, ErrorMessage = "The project description must have less than 80 letters")]
	public string Description { get; set; }
	public string UrlWebsite { get; set; }

	[MinLength(3, ErrorMessage = "The project description must have more than 3 letters")]
	[MaxLength(80, ErrorMessage = "The project description must have less than 80 letters")]
	public string UrlGithub { get; set; }
	public int ViewPriority { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
	public DateTime? StartedAt { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
	public DateTime? FinishedAt { get; set; }
}
