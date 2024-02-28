using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.ViewModels;

public class UpdateSkillViewModel
{
	[Required(ErrorMessage = "The skill id is required")]
	public int Id { get; set; }

	[MinLength(3, ErrorMessage = "The skill title must have more than 3 letters")]
	[MaxLength(80, ErrorMessage = "The skill title must have less than 80 letters")]
	public string Title { get; set; }

	[MinLength(3, ErrorMessage = "The skill description must have more than 3 letters")]
	[MaxLength(80, ErrorMessage = "The skill description must have less than 80 letters")]
	public string Description { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
	public DateTime? Experience { get; set; }

	public string UrlWebsite { get; set; }

	[MinLength(3, ErrorMessage = "The skill color must have more than 3 letters")]
	[MaxLength(80, ErrorMessage = "The skill color must have less than 80 letters")]
	public string Color { get; set; }

	[MinLength(3, ErrorMessage = "The skill icon must have more than 3 letters")]
	[MaxLength(80, ErrorMessage = "The skill icon must have less than 80 letters")]
	public string Icon { get; set; }

	public int ViewPriority { get; set; }
}
