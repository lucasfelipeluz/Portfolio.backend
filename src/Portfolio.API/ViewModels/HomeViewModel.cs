using Portfolio.Services.Dto;

namespace Portfolio.API.ViewModels;

public class HomeViewModel
{
	public List<ProjectDto> Projects { get; set; }
	public List<SkillDto> Skills { get; set; }
	public AboutMeDto AboutMe { get; set; }
	public List<ActivityDto> Activities { get; set; }
}
