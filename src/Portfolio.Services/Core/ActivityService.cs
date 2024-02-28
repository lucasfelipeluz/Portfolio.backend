using AutoMapper;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Cache;
using Portfolio.Infra.Interfaces;
using Portfolio.Services.Dto;
using Portfolio.Services.Interfaces;

namespace Portfolio.Services;

public class ActivityService : IActivityService
{
	private readonly IMapper _mapper;
	private readonly IActivityRepository _activityRepository;
	private readonly ICachingRepository _cachingRepository;

	public ActivityService(IMapper mapper, IActivityRepository activityRepository, ICachingRepository cachingRepository)
	{
		_mapper = mapper;
		_activityRepository = activityRepository;
		_cachingRepository = cachingRepository;
	}

	public async Task<List<ActivityDto>> GetAllActivitiesAsync()
	{
		var cache = _cachingRepository.Get<List<ActivityDto>>(this.ToString());

		if (cache != null)
		{
			return cache;
		}

		var activities = await _activityRepository.GetAllAsync();
		var activitiesDto = _mapper.Map<List<ActivityDto>>(activities);

		_cachingRepository.Save(this.ToString(), activitiesDto);

		return activitiesDto;
	}

	public async Task<ActivityDto> GetActivityByIdAsync(int id)
	{
		var cache = _cachingRepository.Get<ActivityDto>($"{this.ToString()}/{id}");

		if (cache != null)
		{
			return cache;
		}

		var activity = await _activityRepository.GetByIdAsync(id);
		var activityDto = _mapper.Map<ActivityDto>(activity);

		_cachingRepository.Save($"{this.ToString()}/{id}", activityDto);

		return activityDto;
	}

	public async Task<ActivityDto> CreateActivityAsync(ActivityDto entity)
	{
		var activity = _mapper.Map<Activity>(entity);

		await _activityRepository.CreateAsync(activity);

		_cachingRepository.Remove(this.ToString());

		return entity;
	}

	public async Task<bool> UpdateActivityAsync(ActivityDto activityDto)
	{
		var isActivityExists = await _activityRepository.GetByIdAsync(activityDto.Id);

		if (isActivityExists == null)
			return false;

		var activity = _mapper.Map<Activity>(activityDto);
		activity.CreatedAt = isActivityExists.CreatedAt;

		await _activityRepository.UpdateAsync(activity);

		_cachingRepository.Remove(this.ToString());

		return true;
	}

	public async Task<bool> DeleteActivityAsync(int id)
	{
		var activity = await _activityRepository.GetByIdAsync(id);

		if (activity == null)
			return false;

		var response = await _activityRepository.DeleteAsync(id);

		if (!response)
			return false;

		_cachingRepository.Remove(this.ToString());

		return true;
	}
}
