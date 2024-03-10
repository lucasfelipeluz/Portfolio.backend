using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.ViewModels;

public class CreateActivityViewModel
{
	[Required(ErrorMessage = "The activity title is required!")]
	[MinLength(3, ErrorMessage = "The activity title must have more than 3 letters")]
	[MaxLength(100, ErrorMessage = "The activity title must have less than 100 letters")]
	public string Title { get; set; }

	[Required(ErrorMessage = "The activity title in english is required!")]
	[MinLength(3, ErrorMessage = "The activity title in english must have more than 3 letters")]
	[MaxLength(100, ErrorMessage = "The activity title in english must have less than 100 letters")]
	public string TitleEnglish { get; set; }

	[Required(ErrorMessage = "The activity description is required!")]
	[MinLength(3, ErrorMessage = "The activity description must have more than 3 letters")]
	[MaxLength(500, ErrorMessage = "The activity description must have less than 500 letters")]
	public string Description { get; set; }

	[Required(ErrorMessage = "The activity description in english in english is required!")]
	[MinLength(3, ErrorMessage = "The activity description in english must have more than 3 letters")]
	[MaxLength(500, ErrorMessage = "The activity description in english must have less than 500 letters")]
	public string DescriptionEnglish { get; set; }

	[Required(ErrorMessage = "The activity icon is required!")]
	[MinLength(3, ErrorMessage = "The activity icon must have more than 3 letters")]
	public string Icon { get; set; }
}
