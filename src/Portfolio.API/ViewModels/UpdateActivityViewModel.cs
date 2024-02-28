using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.ViewModels;

public class UpdateActivityViewModel
{
	[Required(ErrorMessage = "The activity Id is required!")]
	public int Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string Icon { get; set; }
}
