using Portfolio.Services.Dto;

namespace Portfolio.Services.Interfaces;

public interface IActivityService
{
	Task<List<ActivityDto>> Get();
	Task<ActivityDto> GetById(int id);
	Task<ActivityDto> Create(ActivityDto activityDtoDto);
	Task<ActivityDto> Update(ActivityDto activityDto);
	Task<ActivityDto> Delete(int id);
}
