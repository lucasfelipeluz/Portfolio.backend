using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.ViewModels;

public class CreateSkillViewModel
{
	[Required(ErrorMessage = "The skill title is required!")]
	[MaxLength(100, ErrorMessage = "The skill title must have less than 100 letters")]
	public string Title { get; set; }

	[Required(ErrorMessage = "The skill title in english is required!")]
	[MaxLength(100, ErrorMessage = "The skill title in english must have less than 100 letters")]
	public string TitleEnglish { get; set; }

	[Required(ErrorMessage = "The skill description is required!")]
	[MinLength(3, ErrorMessage = "The skill description must have more than 3 letters")]
	[MaxLength(500, ErrorMessage = "The skill description must have less than 500 letters")]
	public string Description { get; set; }

	[Required(ErrorMessage = "The skill description in english is required!")]
	[MinLength(3, ErrorMessage = "The skill description in english must have more than 3 letters")]
	[MaxLength(500, ErrorMessage = "The skill description in english must have less than 500 letters")]
	public string DescriptionEnglish { get; set; }

	[DataType(DataType.Date)]
	[Required(ErrorMessage = "The skill experience is required!")]
	[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
	public DateTime? Experience { get; set; }

	public string UrlWebsite { get; set; }

	[Required(ErrorMessage = "The skill color is required!")]
	[MinLength(3, ErrorMessage = "The skill color must have more than 3 letters")]
	[MaxLength(80, ErrorMessage = "The skill color must have less than 80 letters")]
	public string Color { get; set; }

	[Required(ErrorMessage = "The skill icon is required!")]
	[MinLength(3, ErrorMessage = "The skill icon must have more than 3 letters")]
	[MaxLength(80, ErrorMessage = "The skill icon must have less than 80 letters")]
	public string Icon { get; set; }

	[Required(ErrorMessage = "The skill view priority is required!")]
	public int ViewPriority { get; set; }
	public int[] ProjectsId { get; set; }
}
