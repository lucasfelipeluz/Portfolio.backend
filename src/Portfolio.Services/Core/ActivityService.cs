using AutoMapper;
using Portfolio.Core.Enums;
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
		var cache = _cachingRepository.Get<List<ActivityDto>>(CacheCode.Activity);

		if (cache is not null)
		{
			return cache;
		}

		var activities = await _activityRepository.GetAllAsync();
		var activitiesDto = _mapper.Map<List<ActivityDto>>(activities);

		_cachingRepository.Save(CacheCode.Activity, activitiesDto);

		return activitiesDto;
	}

	public async Task<ActivityDto> GetActivityByIdAsync(int id)
	{
		var cache = _cachingRepository.Get<List<ActivityDto>>(CacheCode.Activity);

		if (cache is not null)
		{
			var activityCache = cache.Find(x => x.Id == id);

			if (activityCache is not null)
			{
				return activityCache;
			}
		}

		var activity = await _activityRepository.GetByIdAsync(id);
		var activityDto = _mapper.Map<ActivityDto>(activity);

		_cachingRepository.Save(CacheCode.Activity, activityDto);

		return activityDto;
	}

	public async Task<bool> CreateActivityAsync(ActivityDto entity)
	{
		var activity = _mapper.Map<Activity>(entity);

		var isSuccess = await _activityRepository.CreateAsync(activity);

		if (!isSuccess)
			return false;

		_cachingRepository.Remove(CacheCode.Activity);

		return true;
	}

	public async Task<bool> UpdateActivityAsync(ActivityDto activityDto)
	{
		var isActivityExists = await _activityRepository.GetByIdAsync(activityDto.Id);

		if (isActivityExists == null)
			return false;

		var activity = _mapper.Map<Activity>(activityDto);
		activity.CreatedAt = isActivityExists.CreatedAt;

		var isSuccess = await _activityRepository.UpdateAsync(activity);
		if (!isSuccess)
			return false;

		_cachingRepository.Remove(CacheCode.Activity);

		return true;
	}

	public async Task<bool> DeleteActivityAsync(int id)
	{
		var activity = await _activityRepository.GetByIdAsync(id);

		if (activity == null)
			return false;

		var isSuccess = await _activityRepository.DeleteAsync(activity);
		if (!isSuccess)
			return false;

		_cachingRepository.Remove(CacheCode.Activity);

		return true;
	}
}
