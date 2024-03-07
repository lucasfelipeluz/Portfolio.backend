using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Context;

namespace Portfolio.Infra.Interfaces;

public class BaseRepository<T> : IBaseRepository<T>
	where T : Base
{
	private readonly PortfolioContext _context;

	public BaseRepository(PortfolioContext context)
	{
		_context = context;
	}

	public async Task<List<T>> GetAllAsync()
	{
		var entities = await _context.Set<T>().AsNoTracking().ToListAsync();

		return entities;
	}

	public async Task<T> GetByIdAsync(int id)
	{
		var entity = await _context.Set<T>().AsNoTracking().Where(x => x.Id == id).ToListAsync();

		if (entity.Count == 0)
			return null;

		return entity.First();
	}

	public async Task<bool> CreateAsync(T entity)
	{
		_context.Add(entity);
		var result = await _context.SaveChangesAsync();

		if (result <= 0)
			return false;

		return true;
	}

	public async Task<T> CreateAsync(T entity, bool returnEntity)
	{
		_context.Add(entity);
		var result = await _context.SaveChangesAsync();

		if (result <= 0)
			return null;

		return entity;
	}

	public async Task<bool> UpdateAsync(T entity)
	{
		_context.Entry(entity).State = EntityState.Modified;
		var result = await _context.SaveChangesAsync();

		if (result <= 0)
			return false;

		return true;
	}

	public async Task<bool> DeleteAsync(T entity)
	{
		_context.Remove(entity);
		var result = await _context.SaveChangesAsync();

		if (result <= 0)
			return false;

		return true;
	}
}
