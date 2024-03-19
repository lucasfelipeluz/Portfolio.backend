using AutoMapper;
using Portfolio.Core.Enums;
using Portfolio.Core.ExceptionHandles;
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

	public async Task<List<ActivityDto>> Get()
	{
		try
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
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<ActivityDto> GetById(int id)
	{
		try
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

			return activityDto;
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<ActivityDto> Create(ActivityDto entity)
	{
		try
		{
			var activity = _mapper.Map<Activity>(entity);

			var createdActivity = await _activityRepository.CreateAsync(activity);

			_cachingRepository.Remove(CacheCode.Activity);

			return _mapper.Map<ActivityDto>(createdActivity);
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<ActivityDto> Update(ActivityDto activityDto)
	{
		try
		{
			var isActivityExists = await GetById(activityDto.Id);

			if (isActivityExists is null)
				throw new NotFoundEntityException("Activity not found");

			var activity = _mapper.Map<Activity>(activityDto);
			activity.CreatedAt = isActivityExists.CreatedAt;

			var createdActivity = await _activityRepository.UpdateAsync(activity);

			_cachingRepository.Remove(CacheCode.Activity);

			return _mapper.Map<ActivityDto>(createdActivity);
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}

	public async Task<ActivityDto> Delete(int id)
	{
		try
		{
			var activityDto = await GetById(id);

			if (activityDto is null)
				throw new NotFoundEntityException("Activity not found");

			var activity = _mapper.Map<Activity>(activityDto);

			var isSuccess = await _activityRepository.DeleteAsync(activity);

			_cachingRepository.Remove(CacheCode.Activity);

			return activityDto;
		}
		catch (Exception ex)
		{
			throw new ServiceException(ex.Message, ex);
		}
	}
}
