using System.ComponentModel.DataAnnotations;

namespace Portfolio.API.ViewModels;

public class CreateProjectSkillViewModel
{
	[Required(ErrorMessage = "The Skill Id is required!")]
	public int SkillId { get; set; }

	[Required(ErrorMessage = "The Project Id is required!")]
	public int ProjectId { get; set; }
}
