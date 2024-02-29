using Portfolio.Services.Dto;

namespace Portfolio.Services.Interfaces;

public interface IActivityService
{
	Task<List<ActivityDto>> GetAllActivitiesAsync();
	Task<ActivityDto> GetActivityByIdAsync(int id);
	Task<bool> CreateActivityAsync(ActivityDto activityDtoDto);
	Task<bool> UpdateActivityAsync(ActivityDto activityDto);
	Task<bool> DeleteActivityAsync(int id);
}
